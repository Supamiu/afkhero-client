using UnityEngine;
using System.Collections.Generic;

namespace AFKHero.Core.Save
{
	[System.Serializable]
	public class SaveData
	{
		//Système de stats
		public int statPoints = 0;

		//Système de levels
		public double xp = 0;
		public double level = 0;
		public double xpForNextLevel = 5;

		//Stats
		public double agility = 1;
		public double intelligence = 1;
		public double luck = 1;
		public double strength = 1;
		public double vitality = 1;

		//Inventaire
		public double gold = 0;
	}
}
