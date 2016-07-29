using AFKHero.Core.Database;
using AFKHero.Core.Event;
using AFKHero.Core.Gear;
using AFKHero.Core.Save;
using AFKHero.Error;
using AFKHero.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Inventory
{
    public class InventorySystem : MonoBehaviour, Saveable
    {
        public GearSystem gear;

        public Slot[] slots = new Slot[0];

        public int capacity = 24;

        public event Action OnContentChanged;

        void Start()
        {
            SetCapacity(capacity);
            EventDispatcher.Instance.Register("drop", new Listener<GenericGameEvent<Drop>>((ref GenericGameEvent<Drop> drop) =>
            {
                Item i = ItemDatabaseConnector.Instance.GetItem(drop.Data.itemID);
                if (i.GetType() == typeof(Wearable))
                {
                    ((Wearable)i).Roll();
                }
                AddItem(i);
            }));
        }

        /// <summary>
        /// Retire un objet de l'inventaire.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(Item item)
        {
            foreach (Slot slot in slots)
            {
                if (slot.item == item)
                {
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
        }

        public void RemoveAll(Item item)
        {
            foreach (Slot slot in slots)
            {
                if (slot.item == item)
                {
                    slot.Empty();
                    NotifyContentChanged();
                }
            }
        }

        /// <summary>
        /// Détermine si une place de stockage est disponible.
        /// </summary>
        /// <returns></returns>
        public bool HasFreeSlot()
        {
            foreach (Slot slot in slots)
            {
                if (slot.IsFree())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ajoute un item � l'inventaire.
        /// 
        /// SI celui-ci est vide, une InventoryFullException est lev�e.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            foreach (Slot slot in slots)
            {
                if (slot.Store(item))
                {
                    NotifyContentChanged();
                    return;
                }
            }
            throw new InventoryFullException();
        }

        /// <summary>
        /// Triogger l'event de changement de contenu.
        /// </summary>
        private void NotifyContentChanged()
        {
            if (OnContentChanged != null) { OnContentChanged.Invoke(); }
        }

        /// <summary>
        /// Change la capacit� de l'inventaire.
        /// </summary>
        /// <param name="capacity"></param>
        public void SetCapacity(int capacity)
        {
            this.capacity = capacity;
            Slot[] newSlots = new Slot[capacity];
            slots.CopyTo(newSlots, 0);
            for (int i = 0; i < newSlots.Length; i++)
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
        /// Ajoute de la capacit� � l'inventaire.
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
            save.wearableInventory = new List<Wearable>();
            save.consumableInventory = new List<Consumable>();
            save.otherInventory = new List<Item>();
            foreach (Slot slot in slots)
            {
                if (slot.item != null)
                {
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
            }
            return save;
        }

        public void Load(SaveData data)
        {
            SetCapacity(data.capacity);
            foreach (Wearable item in data.wearableInventory)
            {
                AddItem(item);
            }
            foreach (Consumable item in data.consumableInventory)
            {
                AddItem(item);
            }
            foreach (Item item in data.otherInventory)
            {
                AddItem(item);
            }
            NotifyContentChanged();
        }
    }
}
