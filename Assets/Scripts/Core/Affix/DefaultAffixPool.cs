using AFKHero.Core.Gear;
using AFKHero.Model.Affix;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Core.Affix
{
    public class DefaultAffixPool : MonoBehaviour
    {
        /// <summary>
        /// R�cup�re la liste des affixes par d�faut d'un type d'�quipement.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<AffixModel> ForType(GearType type)
        {
            switch (type)
            {
                case GearType.WEAPON:
                    return weapon;

                case GearType.CHEST:
                    return chest;

                case GearType.ARMS:
                case GearType.LEGS:
                case GearType.HEAD:
                    return secondaryArmor;

                case GearType.BOOTS:
                    return boots;

                case GearType.BELT:
                case GearType.NECKLACE:
                case GearType.RING:
                    return accessories;

                default:
                    return new List<AffixModel>();
            }
        }

        private static readonly List<AffixModel> weapon = new List<AffixModel> {
            new AffixModel(AffixType.DAMAGE_BONUS, 0, 10),
             new AffixModel(AffixType.CRIT_DAMAGE_BONUS, 10, 50),
             new AffixModel(AffixType.CRIT_CHANCES_BONUS,1,5)
        };

        private static readonly List<AffixModel> chest = new List<AffixModel> {
            new AffixModel(AffixType.HP_BONUS, 5,20),
            new AffixModel(AffixType.REGEN_BONUS, 10,30),
            new AffixModel(AffixType.DAMAGE_BONUS, 5,10)
    };

        private static readonly List<AffixModel> secondaryArmor = new List<AffixModel> {
            new AffixModel(AffixType.HP_BONUS,1,10),
            new AffixModel(AffixType.CRIT_DAMAGE_BONUS,10,30),
            new AffixModel(AffixType.CRIT_CHANCES_BONUS, 1,5)
        };

        private static readonly List<AffixModel> accessories = new List<AffixModel> {
            new AffixModel(AffixType.HP_BONUS,1,10),
            new AffixModel(AffixType.CRIT_DAMAGE_BONUS,10,30),
            new AffixModel(AffixType.CRIT_CHANCES_BONUS,1,5)
        };

        private static readonly List<AffixModel> boots = new List<AffixModel> {
            new AffixModel(AffixType.HP_BONUS,1,10),
            new AffixModel(AffixType.CRIT_DAMAGE_BONUS,10,30),
            new AffixModel(AffixType.CRIT_CHANCES_BONUS, 1,5),
            new AffixModel(AffixType.MOVESPEED_BONUS, 10,30)
        };

    }
}
