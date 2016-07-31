using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Behaviour;
using AFKHero.Model.Affix;
using System;

namespace AFKHero.Core.Affix.Legendary
{
    public class KickAssRing : ListeningAffixImpl
    {
        public override AffixType GetAffixType()
        {
            return AffixType.LEGENDARY_KICK_ASS_RING;
        }

        public override string GetEventName()
        {
            return "attack.damage";
        }

        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<Damage>>((ref GenericGameEvent<Damage> e) =>
            {
                if (e.Data.attacker.gameObject == gameObject && e.Data.target.gameObject != gameObject)
                {
                    EventDispatcher.Instance.Dispatch("attack.damage",
                        new GenericGameEvent<Damage>(new Damage(gameObject.GetComponent<Agressive>(), gameObject.GetComponent<Damageable>(), value, false, true)));
                }
            }, -100);
        }
    }
}
