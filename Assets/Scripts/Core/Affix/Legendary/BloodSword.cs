using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Behaviour;
using AFKHero.Model.Affix;
using AFKHero.Stat;
using System;

namespace AFKHero.Core.Affix.Legendary
{
    public class BloodSword : ListeningAffixImpl
    {
        public override AffixType GetAffixType()
        {
            return AffixType.LEGENDARY_BLOOD_SWORD;
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
                    double damage = Math.Round(gameObject.GetComponent<Vitality>().Value / 100f * value);
                    e.Data.damage += damage;
                    EventDispatcher.Instance.Dispatch("attack.damage",
                        new GenericGameEvent<Damage>(new Damage(gameObject.GetComponent<Agressive>(), gameObject.GetComponent<Damageable>(), damage, false, true)));
                }
            }, 1500);
        }
    }
}
