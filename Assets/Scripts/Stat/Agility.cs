using UnityEngine;
using System.Collections;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Stat
{
	/// <summary>
	/// Influence la précision et le taux de critiques.
	/// </summary>
	public class Agility : AbstractStat
	{
		// Use this for initialization
		void Start ()
		{
			
		}

		public override void Add (int amount)
		{
			this.amount += amount;
		}

		public override string GetName ()
		{
			return "agility";
		}
	}
}
