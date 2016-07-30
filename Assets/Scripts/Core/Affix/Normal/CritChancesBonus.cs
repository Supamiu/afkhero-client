using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Core.Affix.Normal
{
    public class CritChancesBonus : ListeningAffixImpl
    {
        public override string GetEventName()
        {
            return "attack.compute";
        }

        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> gameEvent) =>
            {
                if (gameEvent.Data.attacker.gameObject == gameObject)
                {
                    gameEvent.Data.critChances += (1 + value / 100f);
                }
            }, 2100);
        }
    }
}
