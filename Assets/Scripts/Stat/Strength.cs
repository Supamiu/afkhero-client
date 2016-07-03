using UnityEngine;
using AFKHero.Core.Event;
using AFKHero.Core.Save;

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

		public override SaveData Save(SaveData data){
			data.strength = this.amount;
			return data;
		}

		public override void DoLoad (SaveData data){
			this.amount = data.strength;
		}

		public override string GetName ()
		{
			return "strength";
		}
	}
}
