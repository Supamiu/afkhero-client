using UnityEngine;
using System.Collections;
using AFKHero.Common;

namespace AFKHero.Stat
{
	public class Vitality : AbstractStat
	{

		public double currentHp;

		public override void Add (int points)
		{
			float ratio = (float)this.currentHp / (float)this.Value;
			this.amount += points;
			this.currentHp = ratio * this.Value;
		}

		public double heal(double amount){
			if (this.currentHp + amount <= this.Value) {
				this.currentHp += amount;
				return amount;
			} else {
				this.currentHp = this.Value;
				return this.Value - this.currentHp;
			}
		}

		void Start ()
		{
			this.currentHp = Value;
		}

		public void Init ()
		{
			this.Start ();
		}

		public override string GetName ()
		{
			return "vitality";
		}
	}
}
