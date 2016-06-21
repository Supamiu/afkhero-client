using UnityEngine;
using System.Collections;

namespace AFKHero.Stat{
	/// <summary>
	/// Influence les damages de l'attaque par clic.
	/// </summary>
	public class Intelligence : AbstractStat {

		public override void Add (int amount)
		{
			this.amount += amount;
		}

		public override string GetName() {
			return "intelligence";
		}
	}
}
