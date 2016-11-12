using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Model.Affix;

namespace AFKHero.Core.Affix.Normal
{
    public class CritChancesBonus : ListeningAffixImpl
    {
        public override AffixType GetAffixType()
        {
            return AffixType.CRIT_CHANCES_BONUS;
        }

        public override string GetEventName()
        {
            return Events.Attack.COMPUTE;
        }

        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> gameEvent) =>
            {
                if (gameEvent.Data.attacker.gameObject == gameObject)
                {
                    gameEvent.Data.critChances += value / 100f;
                }
            }, 2100);
        }
    }
}
