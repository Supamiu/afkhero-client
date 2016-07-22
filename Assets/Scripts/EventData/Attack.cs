using AFKHero.Behaviour;

namespace AFKHero.EventData
{

    public class Attack
	{

		public Attack (Agressive attacker, Damageable target)
		{
			this.target = target;
			this.attacker = attacker;
            baseDamage = attacker.Strength.Value;
		}

		/// <summary>
		/// The base damage.
		/// </summary>
		public double baseDamage;

		/// <summary>
		/// The critical.
		/// </summary>
		public bool critical = false;

		/// <summary>
		/// The critical ratio.
		/// </summary>
		public float criticalRatio = 1.5f;

		/// <summary>
		/// The crit chances.
		/// </summary>
		public float critChances = 0.01f;

		/// <summary>
		/// hits?
		/// </summary>
		public bool hits = true;

		/// <summary>
		/// The target.
		/// </summary>
		public Damageable target;

		/// <summary>
		/// The attacker.
		/// </summary>
		public Agressive attacker;

		/// <summary>
		/// Gets the damage.
		/// </summary>
		/// <returns>The damage.</returns>
		public Damage getDamage ()
		{
			double finalDamage = critical ? baseDamage * criticalRatio : baseDamage;
			return new Damage (attacker, target, finalDamage, critical, hits);
		}
	}
}
