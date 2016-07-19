using UnityEngine;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Model.Affix
{
    [System.Serializable]
    public class DamageBonus : AffixModel
    {
        private Listener<GenericGameEvent<Attack>> listener;

        private GameObject gameObject;

        public DamageBonus()
        {
            listener = new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> gameEvent) =>
            {
                if (gameEvent.Data.attacker.gameObject == gameObject)
                {
                    gameEvent.Data.baseDamage *= (1 + value / 100f);
                }
            }, 50);
        }

        public override void OnAttach(GameObject go)
        {
            gameObject = go;
            EventDispatcher.Instance.Register("attack.compute", listener);
        }

        public override void OnDetach()
        {
            gameObject = null;
            EventDispatcher.Instance.Unregister("attack.compute", listener);
        }
    }
}