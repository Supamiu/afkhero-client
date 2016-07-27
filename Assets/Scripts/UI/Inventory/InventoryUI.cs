using AFKHero.Inventory;
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

        private SlotUI[] currentSlots = new SlotUI[0];

        void Awake()
        {
            inventorySystem.OnContentChanged += () =>
            {
                if (inventorySystem.slots.Length != currentSlots.Length)
                {
                    SetCapacity(inventorySystem.slots.Length);
                }
                for (int i = 0; i < inventorySystem.capacity - 1; i++)
                {
                    currentSlots[i].slot = inventorySystem.slots[i];
                    if(currentSlots[i].wearableDetailsPopup == null)
                    {
                        currentSlots[i].wearableDetailsPopup = wearableDetailsPopup;
                    }
                    currentSlots[i].UpdateDisplay();
                }
            };
        }

        void OnEnable()
        {
            if (inventorySystem.slots.Length != currentSlots.Length)
            {
                SetCapacity(inventorySystem.slots.Length);
            }
            for (int i = 0; i < inventorySystem.slots.Length; i++)
            {
                currentSlots[i].slot = inventorySystem.slots[i];
                if (currentSlots[i].wearableDetailsPopup == null)
                {
                    currentSlots[i].wearableDetailsPopup = wearableDetailsPopup;
                }
                currentSlots[i].UpdateDisplay();
            }
        }

        /// <summary>
        /// Met � jout la capacit� de l'inventaire.
        /// </summary>
        /// <param name="capacity"></param>
        private void SetCapacity(int capacity)
        {
            SlotUI[] newSlots = new SlotUI[capacity];
            currentSlots.CopyTo(newSlots, 0);
            for (int i = 0; i < newSlots.Length; i++)
            {
                SlotUI newSlot = Instantiate(slotPrefab);
                newSlot.transform.SetParent(content.transform);
                newSlot.transform.localScale = Vector3.one;
                newSlots[i] = newSlot;
            }
            currentSlots = newSlots;
        }
    }
}
