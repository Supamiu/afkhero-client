﻿using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Model.Affix
{
    [System.Serializable]
    public class CritDamageBonus : ListeningAffixModel
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
