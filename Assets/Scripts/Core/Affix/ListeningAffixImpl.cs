using UnityEngine;
using AFKHero.Core.Event;

namespace AFKHero.Core.Affix
{
    public abstract class ListeningAffixImpl : AffixImpl
    {
        protected float value;

        private readonly IListener listener;

        public abstract IListener GetListener();

        public abstract string GetEventName();

        protected GameObject gameObject;

        protected ListeningAffixImpl()
        {
            listener = GetListener();
        }

        public override void DoAttach(GameObject go)
        {
            gameObject = go;
            value = model.value;
            EventDispatcher.Instance.Register(GetEventName(), listener);
        }

        public override void Detach()
        {
            EventDispatcher.Instance.Unregister(GetEventName(), listener);
            gameObject = null;
        }
    }
}
