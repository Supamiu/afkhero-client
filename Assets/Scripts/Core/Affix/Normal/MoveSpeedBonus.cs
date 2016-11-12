using AFKHero.Model.Affix;
using UnityEngine;
using AFKHero.Core.Event;

namespace AFKHero.Core.Affix.Normal
{
    public class MoveSpeedBonus : AffixImpl
    {
        public override void Detach()
        {
            EventDispatcher.Instance.Dispatch(Events.Stat.Movespeed.BONUS, new GenericGameEvent<float>(-1f * model.value / 100f));
        }

        public override void DoAttach(GameObject go)
        {
            EventDispatcher.Instance.Dispatch(Events.Stat.Movespeed.BONUS, new GenericGameEvent<float>(model.value / 100f));
        }

        public override AffixType GetAffixType()
        {
            return AffixType.MOVESPEED_BONUS;
        }
    }
}
