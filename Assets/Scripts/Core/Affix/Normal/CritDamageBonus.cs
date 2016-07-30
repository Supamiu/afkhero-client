using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Core.Affix.Normal
{
    public class CritDamageBonus : ListeningAffixImpl
    {
        public override string GetEventName()
        {
            return "attack.compute";
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
