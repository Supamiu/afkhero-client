using AFKHero.Core.Affix.Legendary;
using AFKHero.Core.Affix.Normal;
using AFKHero.Core.Tools;
using AFKHero.Model.Affix;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Core.Affix
{
    public class AffixEngine : Singleton<AffixEngine>
    {
        private Dictionary<AffixType, Type> affixImpls = new Dictionary<AffixType, Type>()
        {
            {AffixType.CRIT_CHANCES_BONUS, typeof(CritChancesBonus) },
            {AffixType.CRIT_DAMAGE_BONUS, typeof(CritDamageBonus) },
            {AffixType.DAMAGE_BONUS, typeof(DamageBonus) },
            {AffixType.HP_BONUS, typeof(HPBonus) },

            {AffixType.LEGENDARY_KICK_ASS_RING, typeof(KickAssRing) }
        };

        public void AttachAffix(AffixModel affix, GameObject go)
        {
            GetImpl(affix.type).Attach(go, affix.value);
        }

        public void DetachAffix(AffixModel affix)
        {
            GetImpl(affix.type).Detach();
        }

        
        private AffixImpl GetImpl(AffixType type)
        {
            Type implType = null;
            affixImpls.TryGetValue(type, out implType);
            //premi�re fois, c'est ptet une l�gendaire !
            if(implType == null)
            {
                Debug.LogError("No implementation for affix type " + type.ToString());
                return null;
            }
            return (AffixImpl)Activator.CreateInstance(implType);
        }
    }
}
