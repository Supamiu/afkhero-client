using System.Collections.Generic;

namespace AFKHero.Core.Gear
{
    public class GearSlot
    {
        //Arme
        public static GearSlot WEAPON = new GearSlot(GearType.WEAPON);

        //Armure
        public static GearSlot HEAD = new GearSlot(GearType.HEAD);
        public static GearSlot CHEST = new GearSlot(GearType.CHEST);
        public static GearSlot ARMS = new GearSlot(GearType.ARMS);
        public static GearSlot LEGS = new GearSlot(GearType.LEGS);
        public static GearSlot BOOTS = new GearSlot(GearType.BOOTS);

        //Accessoires
        public static GearSlot NECKLACE = new GearSlot(GearType.NECKLACE);
        public static GearSlot RING_1 = new GearSlot(GearType.RING);
        public static GearSlot RING_2 = new GearSlot(GearType.RING);
        public static GearSlot BELT = new GearSlot(GearType.BELT);

        public static IEnumerable<GearSlot> Slots
        {
            get
            {
                yield return WEAPON;

                yield return HEAD;
                yield return CHEST;
                yield return ARMS;
                yield return LEGS;
                yield return BOOTS;

                yield return NECKLACE;
                yield return RING_1;
                yield return RING_2;
                yield return BELT;
            }
        }

        public GearType type { get; private set; }

        public string spineSlot { get; set; }

        protected GearSlot(GearType type)
        {
            this.type = type;
        }
    }
}