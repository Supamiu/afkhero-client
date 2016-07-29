using AFKHero.Core.Event;
using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using System.Collections.Generic;
using UnityEngine;
using System;
using AFKHero.Tools;

namespace AFKHero.Model
{
    [Serializable]
    public class Wearable : Item
    {
        public List<AffixModel> affixes;

        public List<AffixModel> affixPool;

        public LegendaryAffixModel legendaryAffix;

        public Sprite sprite;

        public GearType type;

        public int mainStat;

        public float mainStatRatio;

        public int upgrade;

        public void Roll()
        {
            mainStat = RatioEngine.Instance.GetMainStat(this);
            if(affixPool == null)
            {
                return;
            }
            affixes = new List<AffixModel>();
            for (int i = 0; i< GetAffixNumber(rarity); i++)
            {
                AffixModel affix = PercentageUtils.Instance.GetRandomItem(affixPool);
                affixPool.Remove(affix);
                affixes.Add(affix);
            }
            foreach (AffixModel affix in affixes)
            {
                affix.Roll();
            }
            if (rarity == Rarity.LEGENDARY && legendaryAffix != null)
            {
                legendaryAffix.Roll();
            }
        }

        public void Upgrade()
        {
            //TODO
        }

        public void Attach(GameObject go)
        {
            UpdateGearStat(true);
            foreach (AffixModel affix in affixes)
            {
                affix.OnAttach(go);
            }
            if(rarity == Rarity.LEGENDARY && legendaryAffix != null)
            {
                legendaryAffix.OnAttach(go);
            }
        }

        public void Detach()
        {
            UpdateGearStat(false);
            if (affixes == null)
            {
                return;
            }
            foreach (AffixModel affix in affixes)
            {
                affix.OnDetach();
            }
            if (rarity == Rarity.LEGENDARY && legendaryAffix != null)
            {
                legendaryAffix.OnDetach();
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

        private int GetAffixNumber(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.COMMON:
                    return 0;
                case Rarity.MAGIC:
                    return 1;
                case Rarity.RARE:
                    return 2;
                case Rarity.EPIC:
                    return 3;
                case Rarity.LEGENDARY:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
