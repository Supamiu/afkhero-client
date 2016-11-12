using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Behaviour;
using AFKHero.Model.Affix;

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
            return Events.Attack.DAMAGE;
        }

        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<Damage>>((ref GenericGameEvent<Damage> e) =>
            {
                if (e.Data.attacker.gameObject == gameObject && e.Data.target.gameObject != gameObject)
                {
                    EventDispatcher.Instance.Dispatch(Events.Attack.DAMAGE,
                        new GenericGameEvent<Damage>(new Damage(gameObject.GetComponent<Agressive>(), gameObject.GetComponent<Damageable>(), value, false, true)));
                }
            }, -100);
        }
    }
}
