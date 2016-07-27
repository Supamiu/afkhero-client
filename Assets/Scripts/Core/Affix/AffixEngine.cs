using AFKHero.Core.Affix.Legendary;
using AFKHero.Core.Tools;
using AFKHero.Model.Affix;
using AFKHEro.Model.Affix;
using System.Collections.Generic;
using UnityEngine;

namespace AFKHero.Core.Affix
{
    public class AffixEngine : Singleton<AffixEngine>
    {
        private Dictionary<AffixType, AffixImpl> affixImpls = new Dictionary<AffixType, AffixImpl>()
        {
            {AffixType.CRIT_CHANCES_BONUS, new CritChancesBonus() },
            {AffixType.CRIT_DAMAGE_BONUS, new CritDamageBonus() },
            {AffixType.DAMAGE_BONUS, new DamageBonus() },
            {AffixType.HP_BONUS, new HPBonus() },
            {AffixType.LEGENDARY_KICK_ASS_RING, new KickAssRing() }
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
            AffixImpl impl = null;
            affixImpls.TryGetValue(type, out impl);
            if(impl == null)
            {
                Debug.LogError("No implementation for affix type " + type.ToString());
            }
            return impl;
        }
    }
}
