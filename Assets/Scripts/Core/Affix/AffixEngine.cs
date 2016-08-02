using AFKHero.Core.Affix.Legendary;
using AFKHero.Core.Affix.Normal;
using AFKHero.Core.Tools;
using AFKHero.Model.Affix;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AFKHero.Core.Affix
{
    public class AffixEngine : Singleton<AffixEngine>
    {
        private List<AffixImpl> affixImpls;

        private List<AffixImpl> attached = new List<AffixImpl>();
        
        void Awake()
        {
            Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            Type[] result = (from Type type in types where type.IsSubclassOf(typeof(AffixImpl)) && !type.IsAbstract select type).ToArray();
            affixImpls = new List<AffixImpl>();
            foreach(Type t in result)
            {
                affixImpls.Add((AffixImpl)Activator.CreateInstance(t));
            }
        }

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
