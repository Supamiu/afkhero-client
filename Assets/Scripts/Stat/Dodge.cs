using UnityEngine;
using System.Collections;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Behaviour;
using AFKHero.Tools;
using AFKHero.Core.Save;

namespace AFKHero.Stat
{
	/// <summary>
	/// Affecte les chances d'esquiver.
	/// </summary>
	public class Dodge : AbstractStat
	{
		private Damageable damageable;

		void Start ()
		{
			this.damageable = GetComponent<Damageable> ();
			EventDispatcher.Instance.Register ("attack.compute", new Listener<GenericGameEvent<Attack>> ((ref GenericGameEvent<Attack> e) => {
				if (e.Data.target == this.damageable) {
					double precision = e.Data.attacker.GetComponent<Agility> ().Value;
					bool hits = this.GetHits (precision);
					e.Data.hits = hits;
					if (precision > this.Value) {
						float bonus = RatioEngine.Instance.GetCritBonus (precision, this.Value);
						e.Data.critChances += bonus;
					}
				}

			}, 3000));
		}

		public override void Add (int amount)
		{
			this.amount += amount;
		}

		private bool GetHits (double precision)
		{
			return PercentageUtils.Instance.GetResult ((float)precision / (float)this.Value);
		}

		public override string GetName ()
		{
			return "dodge";
		}

		public override SaveData Save(SaveData data){
			return data;
		}

		public override void DoLoad (SaveData data){
		}
	}
}
