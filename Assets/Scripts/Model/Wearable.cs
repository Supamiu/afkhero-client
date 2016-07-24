using AFKHero.Core.Event;
using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Model
{
    [System.Serializable]
    public class Wearable : Item
    {
        public List<AffixModel> affixes;

        public Sprite sprite;

        public GearType type;

        public int mainStat;

        public void Roll()
        {
            if(affixes == null)
            {
                return;
            }
            foreach (AffixModel affix in affixes)
            {
                affix.Roll();
            }
        }

        public void Attach(GameObject go)
        {
            UpdateGearStat(true);
            foreach (AffixModel affix in affixes)
            {
                affix.OnAttach(go);
            }
        }

        public void Detach()
        {
            UpdateGearStat(false);
            foreach (AffixModel affix in affixes)
            {
                affix.OnDetach();
            }
        }

        private void UpdateGearStat(bool equipped)
        {
            int value = equipped ? mainStat : -1 * mainStat;
            if (type == GearType.WEAPON)
            {
                EventDispatcher.Instance.Dispatch("gearstat.attack", new GenericGameEvent<int>(value));
            }
            else
            {
                EventDispatcher.Instance.Dispatch("gearstat.defense", new GenericGameEvent<int>(value));
            }
        }
    }
}
