using UnityEngine;
using AFKHero.Core.Gear;

namespace AFKHero.UI.Inventory
{
    public class GearUI : MonoBehaviour
    {
        public GearSystem gear;
        [Header("Arme")]
        public GearSlotUI weapon;

        [Header("Armure")]
        public GearSlotUI head;
        public GearSlotUI chest;
        public GearSlotUI legs;
        public GearSlotUI arms;
        public GearSlotUI boots;

        [Header("Accessoires")]
        public GearSlotUI necklace;
        public GearSlotUI ring1;
        public GearSlotUI ring2;
        public GearSlotUI belt;

        private void Awake()
        {
            gear.GearChangeEvent += OnContentChanged;
        }

        private void OnEnable()
        {
            OnContentChanged();
        }

        public void OnContentChanged()
        {
            weapon.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.WEAPON));

            head.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.HEAD));
            chest.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.CHEST));
            legs.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.LEGS));
            arms.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.ARMS));
            boots.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.BOOTS));

            necklace.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.NECKLACE));
            ring1.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.RING_1));
            ring2.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.RING_2));
            belt.UpdateDisplay(gear.GetWearableAtSlot(GearSlot.BELT));
        }
    }
}
