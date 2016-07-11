using UnityEngine;
using System.Collections;
using AFKHero.Common;
using AFKHero.Core.Save;
using AFKHero.Core.Event;

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
			if (this.OnVitalityUpdated != null) {
				this.OnVitalityUpdated ();
			}
		}

		public double heal (double amount)
		{
			double healed = amount;
			if (this.currentHp + amount <= this.Value) {
				this.currentHp += amount;
			} else {
				healed = this.Value - this.currentHp;
				this.currentHp = this.Value;
			}
			if (this.OnVitalityUpdated != null) {
				this.OnVitalityUpdated ();
			}
			return healed;
		}

		public delegate void UpdateEvent ();

		public event UpdateEvent OnVitalityUpdated;

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

		public override SaveData Save (SaveData data)
		{
			data.vitality = this.amount;
			return data;
		}

		public override void DoLoad (SaveData data)
		{
			this.amount = data.vitality;
			if (this.OnVitalityUpdated != null) {
				this.OnVitalityUpdated ();
			}
		}
	}
}
