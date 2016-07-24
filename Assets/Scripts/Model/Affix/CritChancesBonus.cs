using AFKHero.Model.Affix;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Model.Affix
{
    [System.Serializable]
    public class CritChancesBonus : ListeningAffixModel
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
