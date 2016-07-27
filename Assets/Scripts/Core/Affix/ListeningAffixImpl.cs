using UnityEngine;
using AFKHero.Core.Event;

namespace AFKHero.Core.Affix
{
    public abstract class ListeningAffixImpl : AffixImpl
    {
        protected float value;

        private IListener listener;

        public abstract IListener GetListener();

        public abstract string GetEventName();

        protected GameObject gameObject;

        public ListeningAffixImpl()
        {
            this.listener = GetListener();
        }

        public override void Attach(GameObject go, float value)
        {
            gameObject = go;
            this.value = value;
            EventDispatcher.Instance.Register(GetEventName(), listener);
        }

        public override void Detach()
        {
            EventDispatcher.Instance.Unregister(GetEventName(), listener);
            gameObject = null;
        }
    }
}
