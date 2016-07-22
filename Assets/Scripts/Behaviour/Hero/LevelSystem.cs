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

		private double xp = 0;

		public double xpForNextLevel = 5;

		void Start ()
		{
			EventDispatcher.Instance.Register ("experience", new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
                ReceiveXp(e.Data);
			}));
			EventDispatcher.Instance.Dispatch ("level.update", new GenericGameEvent<LevelUp> (new LevelUp (level, GetXpForLevel(level), xp)));
		}

		void ReceiveXp (double amount)
		{
			
			while (xp + amount >= xpForNextLevel) {
				amount -= xpForNextLevel - xp;
                xp = 0;
                level++;
                xpForNextLevel = GetXpForLevel(level);
				EventDispatcher.Instance.Dispatch ("level.up", new GenericGameEvent<LevelUp> (new LevelUp (level, GetXpForLevel(level), xp)));
			}
            xp += amount;
			EventDispatcher.Instance.Dispatch ("experience.ui", new GenericGameEvent<XPGain> (new XPGain (xp, xpForNextLevel)));
		}

		private double GetXpForLevel (double level)
		{
			return Math.Round (5 * (Math.Pow (level, 1.5f)));
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
		}
	}
}
