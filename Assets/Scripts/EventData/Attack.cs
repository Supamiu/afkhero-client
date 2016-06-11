using UnityEngine;
using System.Collections;
using AFKHero.Behaviour;
using AFKHero.Stat;

namespace AFKHero.EventData
{
	
	public class Attack
	{

		public Attack (Agressive attacker, Damageable target)
		{
			this.target = target;
			this.attacker = attacker;
			this.baseDamage = attacker.Strength.Value;
		}

		public double baseDamage;

		public bool critical = false;

		public float criticalRatio = 1.5f;

		public Damageable target;

		public Agressive attacker;

		public Damage getDamage ()
		{
			double finalDamage = this.critical ? this.baseDamage * this.criticalRatio : this.baseDamage;
			return new Damage (this.attacker, this.target, finalDamage, this.critical);
		}
	}
}
