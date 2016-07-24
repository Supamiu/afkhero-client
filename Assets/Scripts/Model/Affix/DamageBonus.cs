using AFKHero.Core.Event;
using AFKHero.EventData;
using System;

namespace AFKHero.Model.Affix
{
    [Serializable]
    public class DamageBonus : ListeningAffixModel
    {
        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> gameEvent) =>
            {
                if (gameEvent.Data.attacker.gameObject == gameObject)
                {
                    gameEvent.Data.damageBonusFactor += value/100f;
                }
            }, 50);
        }

        public override string GetEventName()
        {
            return "attack.compute";
        }
    }
}
