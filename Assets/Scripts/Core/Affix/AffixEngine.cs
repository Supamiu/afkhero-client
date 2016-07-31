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
        private List<AffixImpl> affixImpls = new List<AffixImpl>()
        {
            {new CritChancesBonus() },
            {new CritDamageBonus() },
            {new DamageBonus() },
            {new HPBonus() },

            {new KickAssRing() }
        };

        private List<AffixImpl> attached = new List<AffixImpl>();

        public void AttachAffix(AffixModel affix, GameObject go)
        {
            AffixImpl attached = GetImpl(affix.type);
            attached.Attach(go, affix);
            this.attached.Add(attached);
        }

        public void DetachAffix(AffixModel affix)
        {
            foreach (AffixImpl a in attached)
            {
                if (a.model.Equals(affix))
                {
                    a.Detach();
                    attached.Remove(a);
                    return;
                }
            }
        }


        private AffixImpl GetImpl(AffixType type)
        {
            AffixImpl implType = null;
            foreach (AffixImpl impl in affixImpls)
            {
                if (impl.GetAffixType() == type)
                {
                    implType = impl;
                }
            }
            if (implType == null)
            {
                Debug.LogError("No implementation for affix type " + type.ToString());
                return null;
            }
            return (AffixImpl)Activator.CreateInstance(implType.GetType());
        }
    }
}
