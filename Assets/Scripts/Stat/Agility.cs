using UnityEngine;
using System.Collections;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Core.Save;

namespace AFKHero.Stat
{
	/// <summary>
	/// Influence la précision et le taux de critiques.
	/// </summary>
	public class Agility : AbstractStat
	{
		public override void Add (int amount)
		{
			this.amount += amount;
		}

		public override SaveData Save(SaveData data){
			data.agility = this.amount;
			return data;
		}

		public override void DoLoad (SaveData data){
			this.amount = data.agility;
		}

		public override string GetName ()
		{
			return "agility";
		}
	}
}
