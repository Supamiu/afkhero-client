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

        private readonly List<AffixImpl> attached = new List<AffixImpl>();

        private void Awake()
        {
            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            var result = (from Type type in types where type.IsSubclassOf(typeof(AffixImpl)) && !type.IsAbstract select type).ToArray();
            affixImpls = new List<AffixImpl>();
            foreach(var t in result)
            {
                affixImpls.Add((AffixImpl)Activator.CreateInstance(t));
            }
        }

        /// <summary>
        /// Permet d'attacher l'affixe sur le gameobject.
        /// </summary>
        /// <param name="affix"></param>
        /// <param name="go"></param>
        public void AttachAffix(AffixModel affix, GameObject go)
        {
            var attachedImpl = GetImpl(affix.type);
            attachedImpl.Attach(go, affix);
            attached.Add(attachedImpl);
        }

        /// <summary>
        /// Détache l'affixe de son gameobject.
        /// </summary>
        /// <param name="affix"></param>
        public void DetachAffix(AffixModel affix)
        {
            foreach (var a in attached)
            {
                if (!a.model.Equals(affix)) continue;
                a.Detach();
                attached.Remove(a);
                return;
            }
        }

        /// <summary>
        /// Récupère l'implémentation d'une affixe en fonction de son type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private AffixImpl GetImpl(AffixType type)
        {
            AffixImpl implType = null;
            foreach (var impl in affixImpls)
            {
                if (impl.GetAffixType() == type)
                {
                    implType = impl;
                }
            }
            if (implType != null) return (AffixImpl) Activator.CreateInstance(implType.GetType());
            Debug.LogError("No implementation for affix type " + type);
            return null;
        }
    }
}
