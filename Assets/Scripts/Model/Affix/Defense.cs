using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Model.Affix
{
    public class Defense : ListeningAffixModel
    {
        public override string GetEventName()
        {
            return "stat.compute.defense";
        }

        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<StatCompute>>((ref GenericGameEvent<StatCompute> gameEvent) => {
                if(gameEvent.Data.statOwner == gameObject)
                {
                    gameEvent.Data.amount += value;
                }
            });
        }
    }
}
