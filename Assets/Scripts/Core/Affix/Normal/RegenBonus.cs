using AFKHero.Core.Event;
using AFKHero.Model.Affix;

namespace AFKHero.Core.Affix.Normal
{
    public class RegenBonus : ListeningAffixImpl
    {
        public override AffixType GetAffixType()
        {
            return AffixType.REGEN_BONUS;
        }

        public override string GetEventName()
        {
            return Events.Stat.Regen.BONUS;
        }

        public override IListener GetListener()
        {
            return new Listener<GenericGameEvent<float>>((ref GenericGameEvent<float> e) =>
            {
                e.Data += value / 100f;
            });
        }
    }
}
