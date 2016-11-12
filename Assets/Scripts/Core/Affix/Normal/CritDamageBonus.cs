using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Model.Affix;

namespace AFKHero.Core.Affix.Normal
{
    public class CritDamageBonus : ListeningAffixImpl
    {
        public override AffixType GetAffixType()
        {
            return AffixType.CRIT_DAMAGE_BONUS;
        }

        public override string GetEventName()
        {
            return Events.Attack.COMPUTE;
        }

        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> gameEvent) =>
            {
                gameEvent.Data.criticalRatio += value / 100f;
            }, 2200);
        }
    }
}
