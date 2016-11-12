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
        private GameObject go;

        public void Roll()
        {
            mainStat = RatioEngine.Instance.GetMainStat(this);
            if (affixPool == null)
            {
                return;
            }
            affixes = new List<AffixModel>();
            for (var i = 0; i < GetAffixNumber(rarity); i++)
            {
                AddAffix();
            }
            foreach (var affix in affixes)
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
            if (upgrade >= 12) return false;
            if (!PercentageUtils.Instance.GetResult(RatioEngine.Instance.GetUpgradeChances(this))) return false;
            mainStat = RatioEngine.Instance.GetUpgradedStat(this);
            upgrade++;
            //Si c'est un multiple de 3, on change de qualité :)
            if (upgrade % 3 != 0 || rarity >= Rarity.EPIC) return true;
            rarity++;
            var affix = AddAffix();
            affix.Roll();
            if (go != null)
            {
                affix.OnAttach(go);
            }
            return true;
        }

        private AffixModel AddAffix()
        {
            var affix = PercentageUtils.Instance.GetRandomItem(affixPool);
            affixPool.Remove(affix);
            affixes.Add(affix);
            return affix;
        }

        public void Attach(GameObject pGo)
        {
            go = pGo;
            UpdateGearStat(true);
            foreach (var affix in affixes)
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
            foreach (var affix in affixes)
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
            var value = equipped ? mainStat : -1 * mainStat;
            EventDispatcher.Instance.Dispatch(type == GearType.WEAPON ? Events.GearStat.ATTACK : Events.GearStat.DEFENSE,
                new GenericGameEvent<GearStat>(new GearStat(value, go)));
        }

        private static int GetAffixNumber(Rarity pRarity)
        {
            switch (pRarity)
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
