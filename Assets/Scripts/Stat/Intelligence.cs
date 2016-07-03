using UnityEngine;
using System.Collections;
using AFKHero.Core.Save;

namespace AFKHero.Stat{
	/// <summary>
	/// Influence les damages de l'attaque par clic.
	/// </summary>
	public class Intelligence : AbstractStat {

		public override void Add (int amount)
		{
			this.amount += amount;
		}

		public override SaveData Save(SaveData data){
			data.intelligence = this.amount;
			return data;
		}

		public override void DoLoad (SaveData data){
			this.amount = data.intelligence;
		}

		public override string GetName() {
			return "intelligence";
		}
	}
}
