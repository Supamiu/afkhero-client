using AFKHero.Common;
using AFKHero.Inventory;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AFKHero.UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public SlotUI slotPrefab;

        public WearableDetails wearableDetailsPopup;

        public InventorySystem inventorySystem;

        public GridLayoutGroup content;

        public Text dustValue;

        private SlotUI[] currentSlots = new SlotUI[0];

        private Action OnContentChanged;

        private void Awake()
        {
            OnContentChanged = () =>
            {
                if (inventorySystem.slots.Length != currentSlots.Length)
                {
                    SetCapacity(inventorySystem.slots.Length);
                }
                for (var i = 0; i < inventorySystem.capacity - 1; i++)
                {
                    currentSlots[i].slot = inventorySystem.slots[i];
                    if(currentSlots[i].wearableDetailsPopup == null)
                    {
                        currentSlots[i].wearableDetailsPopup = wearableDetailsPopup;
                    }
                    currentSlots[i].UpdateDisplay();
                }
                dustValue.text = Formatter.Format(inventorySystem.dust);
            };
            inventorySystem.OnContentChanged += OnContentChanged;
        }

        private void OnEnable()
        {
            OnContentChanged.Invoke();
        }

        /// <summary>
        /// Met � jout la capacit� de l'inventaire.
        /// </summary>
        /// <param name="capacity"></param>
        private void SetCapacity(int capacity)
        {
            var newSlots = new SlotUI[capacity];
            currentSlots.CopyTo(newSlots, 0);
            for (var i = 0; i < newSlots.Length; i++)
            {
                var newSlot = Instantiate(slotPrefab);
                newSlot.transform.SetParent(content.transform);
                newSlot.transform.localScale = Vector3.one;
                newSlots[i] = newSlot;
            }
            currentSlots = newSlots;
        }
    }
}
