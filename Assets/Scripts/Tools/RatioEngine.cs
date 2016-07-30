using UnityEngine;
using AFKHero.Core.Tools;
using AFKHero.Model;
using AFKHero.Core.Gear;

namespace AFKHero.Tools
{
    public class RatioEngine : Singleton<RatioEngine>
    {

        public double GetEnemyDamage(float damageRatio, float distance)
        {
            return (int)(damageRatio * (Mathf.Pow(distance - 10 / 2, 1.2f)) / 10);
        }

        public double GetEnemyHealth(float healthRatio, float distance)
        {
            return (int)(healthRatio * (Mathf.Pow(distance - 10, 1.5f) / 20) - 2);
        }

        public double GetEnemyDodge(float dodgeRatio, float distance)
        {
            if (distance < 50f)
            {
                distance = 0;
            }
            return dodgeRatio * distance / 10f;
        }

        public float GetCritBonus(double precision, double dodge)
        {
            return ((float)precision - (float)dodge) / 100f;
        }

        public double GetEnemyDefense(double baseValue, float distance)
        {
            return baseValue * 1 + (distance * AFKHero.Config.DEFENSE_BONUS_PER_METER);
        }

        public float GetDamageReductionPercent(double value)
        {
            return (float)value / (5000f + (float)value);
        }

        public int GetMainStat(Wearable wearable)
        {
            float ratio = Editor.GetMainStatRatio(wearable);
            float baseValue = 10 + ratio * AFKHero.GetDistance() * wearable.mainStatRatio;
            return Mathf.CeilToInt(Random.Range(baseValue * .90f, baseValue * 1.10f));
        }


        //Tout ce qui concerne l'upgrade de wearables.
        public double GetDust(Wearable w)
        {
            return w.mainStat / 8 * ((int)w.rarity + 1);
        }

        public int GetUpgradedStat(Wearable w)
        {
            return 1 + Mathf.RoundToInt(w.mainStat * (1 + ((int)w.rarity + 1) / 100f));
        }

        public double GetUpgradeCost(Wearable w)
        {
            return 1 + Mathf.Floor(((int)w.rarity + 1f) / 2f * Mathf.Pow(w.upgrade, 1.8f));
        }

        public float GetUpgradeChances(Wearable w)
        {
            return 1f - w.upgrade / 4f;
        }


        /// <summary>
        /// Regroupe les fonctionnalit�s li�es � l'�diteur.
        /// </summary>
        public class Editor
        {
            public static float GetDefaultMainStatRatio(GearType type)
            {
                switch (type)
                {
                    case GearType.WEAPON:
                        return 1f;
                    case GearType.RING:
                        return .1f;
                    case GearType.NECKLACE:
                        return .1f;
                    case GearType.LEGS:
                        return .5f;
                    case GearType.HEAD:
                        return .3f;
                    case GearType.BOOTS:
                        return .3f;
                    case GearType.CHEST:
                        return .5f;
                    case GearType.BELT:
                        return .2f;
                    case GearType.ARMS:
                        return .3f;
                    default:
                        return 0f;
                }
            }

            public static float GetMainStatRatio(Wearable w)
            {
                switch (w.rarity)
                {
                    case Rarity.COMMON:
                        return 0.016f * w.mainStatRatio;
                    case Rarity.MAGIC:
                        return 0.02f * w.mainStatRatio;
                    case Rarity.RARE:
                        return 0.025f * w.mainStatRatio;
                    case Rarity.EPIC:
                        return 0.033f * w.mainStatRatio;
                    case Rarity.LEGENDARY:
                        return 0.05f * w.mainStatRatio;
                    default:
                        return 0f;
                }
            }
        }
    }
}
