using UnityEngine;
using System.Collections;
using AFKHero.Behaviour;

namespace AFKHero.EventData{
	
	public class Attack {

		public Attack(double damage, Damageable target, Agressive attacker){
			this.damage = damage;
			this.target = target;
			this.attacker = attacker;
		}

		public double damage;

		public bool critical = false;

		public double criticalDamage = 0;

		public Damageable target;

		public Agressive attacker;

		public double getDamage(){
			return this.critical ? criticalDamage : damage;
		}
	}
}
