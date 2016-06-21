using UnityEngine;
using System.Collections;

namespace AFKHero.Stat
{
	/// <summary>
	/// Affacte les taux de loot.
	/// </summary>
	public class Luck : AbstractStat
	{

		public override void Add (int amount)
		{
			this.amount += amount;
		}

		public override string GetName ()
		{
			return "luck";
		}
	}
}
