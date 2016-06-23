using UnityEngine;
using System.Collections;
using AFKHero.Behaviour;

namespace AFKHero.EventData
{
	public class Heal
	{
		public double amount;

		public Regen target;

		public Heal (double amount, Regen target)
		{
			this.amount = amount;
			this.target = target;
		}
	}
}
