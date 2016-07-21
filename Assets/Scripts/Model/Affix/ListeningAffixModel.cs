using UnityEngine;
using AFKHero.Core.Event;

namespace AFKHero.Model.Affix
{
    [System.Serializable]
    public abstract class ListeningAffixModel : AffixModel
    {
        public abstract IListener GetListener();

        public abstract string GetEventName();

        protected GameObject gameObject;

        public override void OnAttach(GameObject go)
        {
            gameObject = go;
            EventDispatcher.Instance.Register(GetEventName(), GetListener());
        }

        public override void OnDetach()
        {
            gameObject = null;
            EventDispatcher.Instance.Unregister(GetEventName(), GetListener());
        }
    }
}
