using AFKHero.Core.Event;
using AFKHero.Error;
using AFKHero.Model;
using System;
using UnityEngine;

namespace AFKHero.Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        public Slot[] slots = new Slot[0];

        public int capacity = 0;

        public event Action OnContentChanged;

        void Start()
        {
            SetCapacity(capacity);
            EventDispatcher.Instance.Register("drop.wearable", new Listener<GenericGameEvent<WearableDrop>>((ref GenericGameEvent<WearableDrop> drop) => {
                Wearable i = drop.Data.item;
                i.Roll();
                AddItem(i);
            }));
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
            if(OnContentChanged != null) { OnContentChanged.Invoke(); }
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
            for(int i = 0; i<newSlots.Length; i++)
            {
                if(newSlots[i] == null)
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
    }
}
