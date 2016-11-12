using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Model.Affix;

namespace AFKHero.Core.Affix.Normal
{
    public class HPBonus : ListeningAffixImpl
    {
        public override AffixType GetAffixType()
        {
            return AffixType.HP_BONUS;
        }

        public override string GetEventName()
        {
            return Events.Stat.Vitality.COMPUTE;
        }

        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<StatCompute>>((ref GenericGameEvent<StatCompute> gameEvent) =>
            {
                if (gameEvent.Data.statOwner == gameObject)
                {
                    gameEvent.Data.ratio += gameEvent.Data.stat.ratio * value / 100f;
                }
            }); ;
        }
    }
}
