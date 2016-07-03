using UnityEngine;
using System.Collections;
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
				this.ReceiveXp (e.Data);
			}));
			EventDispatcher.Instance.Dispatch ("level.update", new GenericGameEvent<LevelUp> (new LevelUp (this.level, this.GetXpForLevel (this.level), this.xp)));
		}

		void ReceiveXp (double amount)
		{
			
			while (this.xp + amount >= this.xpForNextLevel) {
				amount -= this.xpForNextLevel - this.xp;
				this.xp = 0;
				this.level++;
				this.xpForNextLevel = this.GetXpForLevel (this.level);
				EventDispatcher.Instance.Dispatch ("level.up", new GenericGameEvent<LevelUp> (new LevelUp (this.level, this.GetXpForLevel (this.level), this.xp)));
			}
			this.xp += amount;
			EventDispatcher.Instance.Dispatch ("experience.ui", new GenericGameEvent<XPGain> (new XPGain (this.xp, this.xpForNextLevel)));
		}

		private double GetXpForLevel (double level)
		{
			return Math.Round (5 * (Math.Pow (level, 1.5f)));
		}

		public SaveData Save (SaveData save)
		{
			save.level = this.level;
			save.xp = this.xp;
			save.xpForNextLevel = this.xpForNextLevel;
			return save;
		}

		public void Load (SaveData save)
		{
			this.level = save.level;
			this.xp = save.xp;
			this.xpForNextLevel = save.xpForNextLevel;
		}
	}
}
