using UnityEngine;
using AFKHero.Core.Event;

namespace AFKHero.Stat
{
	/// <summary>
	/// Affecte les damages d'attaque;
	/// </summary>
	public class Strength : AbstractStat
	{
		public override void Add (int amount)
		{
			this.amount += amount;
		}

		public override string GetName ()
		{
			return "strength";
		}
	}
}
