using AFKHero.Core.Event;
using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using System.Collections.Generic;
using UnityEngine;
using System;
using AFKHero.Tools;
using AFKHero.EventData;

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

        public bool customRatio;

        public bool customAffixPool;

        [NonSerialized]
        private GameObject go = null;

        public void Roll()
        {
            mainStat = RatioEngine.Instance.GetMainStat(this);
            if (affixPool == null)
            {
                return;
            }
            affixes = new List<AffixModel>();
            for (int i = 0; i < GetAffixNumber(rarity); i++)
            {
                AddAffix();
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

        public bool Upgrade()
        {
            if (upgrade < 12)
            {
                if (PercentageUtils.Instance.GetResult(RatioEngine.Instance.GetUpgradeChances(this)))
                {
                    mainStat = RatioEngine.Instance.GetUpgradedStat(this);
                    upgrade++;
                    //Si c'est un multiple de 3, on change de qualit� :)
                    if (upgrade % 3 == 0 && rarity < Rarity.EPIC)
                    {
                        rarity++;
                        AffixModel affix = AddAffix();
                        affix.Roll();
                        if (go != null)
                        {
                            affix.OnAttach(go);
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        private AffixModel AddAffix()
        {
            AffixModel affix = PercentageUtils.Instance.GetRandomItem(affixPool);
            affixPool.Remove(affix);
            affixes.Add(affix);
            return affix;
        }

        public void Attach(GameObject go)
        {
            this.go = go;
            UpdateGearStat(true);
            foreach (AffixModel affix in affixes)
            {
                affix.OnAttach(go);
            }
            if (rarity == Rarity.LEGENDARY && legendaryAffix != null)
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

        public bool IsUpgradeable()
        {
            return upgrade >= 0;
        }

        private void UpdateGearStat(bool equipped)
        {
            int value = equipped ? mainStat : -1 * mainStat;
            if (type == GearType.WEAPON)
            {
                EventDispatcher.Instance.Dispatch("gearstat.attack", new GenericGameEvent<GearStat>(new GearStat(value, go)));
            }
            else
            {
                EventDispatcher.Instance.Dispatch("gearstat.defense", new GenericGameEvent<GearStat>(new GearStat(value, go)));
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
                case Rarity.LEGENDARY:
                    return 3;
                default:
                    return 0;
            }
        }
    }
}
