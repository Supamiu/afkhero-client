using UnityEngine;
using AFKHero.Core.Event;
using System;
using AFKHero.EventData;
using AFKHero.Core.Save;

namespace AFKHero.Behaviour.Hero
{
    public class LevelSystem : MonoBehaviour, Saveable
	{
		private double level = 1;

	    private double xp;

		public double xpForNextLevel = 5;

	    private void Start ()
		{
			EventDispatcher.Instance.Register (Events.EXPERIENCE_GAIN, new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
                ReceiveXp(e.Data);
			}));
			EventDispatcher.Instance.Dispatch (Events.Level.UPDATE, new GenericGameEvent<LevelUp> (new LevelUp (level, GetXpForLevel(level), xp)));
		}

	    private void ReceiveXp (double amount)
		{
			
			while (xp + amount >= xpForNextLevel) {
				amount -= xpForNextLevel - xp;
                xp = 0;
                level++;
                xpForNextLevel = GetXpForLevel(level);
				EventDispatcher.Instance.Dispatch (Events.Level.UP, new GenericGameEvent<LevelUp> (new LevelUp (level, GetXpForLevel(level), xp)));
			}
            xp += amount;
			EventDispatcher.Instance.Dispatch (Events.UI.EXPERIENCE, new GenericGameEvent<XPGain> (new XPGain (xp, xpForNextLevel)));
		}

		private static double GetXpForLevel (double pLevel)
		{
			return Math.Round (5 * Math.Pow (pLevel, 1.5f));
		}

		public SaveData Save (SaveData save)
		{
			save.level = level;
			save.xp = xp;
			save.xpForNextLevel = xpForNextLevel;
			return save;
		}

		public void Load (SaveData save)
		{
            level = save.level;
            xp = save.xp;
			xpForNextLevel = save.xpForNextLevel;

            EventDispatcher.Instance.Dispatch(Events.Level.UPDATE, new GenericGameEvent<LevelUp>(new LevelUp(level, GetXpForLevel(level), xp)));
            EventDispatcher.Instance.Dispatch(Events.UI.EXPERIENCE, new GenericGameEvent<XPGain>(new XPGain(xp, xpForNextLevel)));
        }
	}
}
