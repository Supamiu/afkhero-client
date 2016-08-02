using AFKHero.Behaviour;
using System;

namespace AFKHero.EventData
{
    public class Damage
	{

		public double damage{ get; set; }

		public bool critical{ get; private set; }

		public bool hits { get; private set; }

		public Damageable target{ get; private set; }

		public Agressive attacker { get; private set; }

		public Damage (Agressive attacker, Damageable target, double damage, bool crit, bool hits)
		{
			this.damage = Math.Round (damage);
            critical = crit;
			this.target = target;
			this.attacker = attacker;
			this.hits = hits;
		}
	}
}
