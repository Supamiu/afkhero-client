using UnityEngine;
using System.Collections;
using AFKHero.Core.Save;

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

		public override SaveData Save(SaveData data){
			data.luck = this.amount;
			return data;
		}

		public override void DoLoad (SaveData data){
			this.amount = data.luck;
		}

		public override string GetName ()
		{
			return "luck";
		}
	}
}
