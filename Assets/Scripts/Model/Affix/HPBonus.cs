using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Model.Affix
{
    public class HPBonus : ListeningAffixModel
    {
        public override string GetEventName()
        {
            return "stat.compute.vitality";
        }

        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<StatCompute>>((ref GenericGameEvent<StatCompute> gameEvent) =>
            {
                if (gameEvent.Data.statOwner == gameObject)
                {
                    gameEvent.Data.ratio += gameEvent.Data.stat.ratio * value / 100f;
                }
            });
        }
    }
}
