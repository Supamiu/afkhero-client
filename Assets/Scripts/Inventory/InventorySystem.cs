using AFKHero.Core.Database;
using AFKHero.Core.Event;
using AFKHero.Core.Gear;
using AFKHero.Core.Save;
using AFKHero.Error;
using AFKHero.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFKHero.Inventory
{
    public class InventorySystem : MonoBehaviour, Saveable
    {
        public GearSystem gear;

        public Slot[] slots = new Slot[0];

        public int capacity = 24;

        public double dust;

        public event Action OnContentChanged;

        private void Start()
        {
            SetCapacity(capacity);
            EventDispatcher.Instance.Register(Events.DROP, new Listener<GenericGameEvent<Drop>>((ref GenericGameEvent<Drop> drop) =>
            {
                var i = ItemDatabaseConnector.Instance.GetItem(drop.Data.itemID);
                if (i.GetType() == typeof(Wearable))
                {
                    ((Wearable)i).Roll();
                }
                AddItem(i);
            }));

            EventDispatcher.Instance.Register(Events.DUST, new Listener<GenericGameEvent<double>>((ref GenericGameEvent<double> e) =>
            {
                dust += e.Data;
                NotifyContentChanged();
            }));
        }

        /// <summary>
        /// Retire un objet de l'inventaire.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Item item)
        {
            foreach (var slot in slots)
            {
                if (slot.item != item) continue;
                if (slot.stack > 1)
                {
                    slot.RemoveOne();
                }
                else
                {
                    slot.Empty();
                }
                NotifyContentChanged();
            }
        }

        public void RemoveAll(Item item)
        {
            foreach (var slot in slots)
            {
                if (slot.item != item) continue;
                slot.Empty();
                NotifyContentChanged();
            }
        }

        /// <summary>
        /// Détermine si une place de stockage est disponible.
        /// </summary>
        /// <returns></returns>
        public bool HasFreeSlot()
        {
            return slots.Any(slot => slot.IsFree());
        }

        /// <summary>
        /// Ajoute un item à l'inventaire.
        /// 
        /// SI celui-ci est vide, une InventoryFullException est levée.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            //Il semble y avoir un bug lors de la sérialization, ça devrait le fix.
            if(item.itemName.Length < 1 || item.icon == null)
            {
                return;
            }
            if (!slots.Any(slot => slot.Store(item))) throw new InventoryFullException();
            NotifyContentChanged();
        }

        /// <summary>
        /// Triogger l'event de changement de contenu.
        /// </summary>
        private void NotifyContentChanged()
        {
            if (OnContentChanged != null) { OnContentChanged.Invoke(); }
        }

        /// <summary>
        /// Change la capacité de l'inventaire.
        /// </summary>
        /// <param name="pCapacity"></param>
        public void SetCapacity(int pCapacity)
        {
            capacity = pCapacity;
            var newSlots = new Slot[capacity];
            slots.CopyTo(newSlots, 0);
            for (var i = 0; i < newSlots.Length; i++)
            {
                if (newSlots[i] == null)
                {
                    newSlots[i] = new Slot();
                }
            }
            slots = newSlots;
            NotifyContentChanged();
        }

        /// <summary>
        /// Ajoute de la capacité à l'inventaire.
        /// </summary>
        /// <param name="amount"></param>
        public void AddCapacity(int amount)
        {
            capacity += amount;
            SetCapacity(capacity);
        }

        public SaveData Save(SaveData save)
        {
            save.capacity = capacity;
            save.dust = dust;
            save.wearableInventory = new List<Wearable>();
            save.consumableInventory = new List<Consumable>();
            save.otherInventory = new List<Item>();
            foreach (var slot in slots)
            {
                if (slot.item == null) continue;
                if (slot.item.GetType() == typeof(Wearable))
                {
                    save.wearableInventory.Add((Wearable)slot.item);
                }
                else if (slot.item.GetType() == typeof(Consumable))
                {
                    save.consumableInventory.Add((Consumable)slot.item);
                }
                else
                {
                    save.otherInventory.Add(slot.item);
                }
            }
            return save;
        }

        public void Load(SaveData data)
        {
            slots = new Slot[capacity];
            SetCapacity(data.capacity);
            dust = data.dust;
            foreach (var item in data.wearableInventory)
            {
                AddItem(item);
            }
            foreach (var item in data.consumableInventory)
            {
                AddItem(item);
            }
            foreach (var item in data.otherInventory)
            {
                AddItem(item);
            }
            NotifyContentChanged();
        }
    }
}
