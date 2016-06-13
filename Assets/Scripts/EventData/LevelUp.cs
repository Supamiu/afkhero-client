using UnityEngine;
using System.Collections;

namespace AFKHero.EventData
{
	public class LevelUp
	{
		public double level;

		public double xpForNextLevel;

		public double xpRemaining;

		public LevelUp(double level, double xpForNext, double xp){
			this.level = level;
			this.xpForNextLevel = xpForNext;
			this.xpRemaining = xp;
		}
	}
}
