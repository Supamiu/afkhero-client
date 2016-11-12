using AFKHero.Behaviour;

namespace AFKHero.EventData
{

    public class Attack
    {

        public Attack(Agressive attacker, Damageable target)
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
        public float critChances = .01f;

        /// <summary>
        /// hits?
        /// </summary>
        public bool hits = true;

        /// <summary>
        /// La réduction de damage causée par la défense de la cible.
        /// </summary>
        public float damageReductionPercent = 0;

        /// <summary>
        /// Le bonus de damage total.
        /// </summary>
        public float damageBonusFactor = 0f;

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
        public Damage getDamage()
        {
            double finalDamage;
            if (critical)
            {
                finalDamage = (baseDamage * criticalRatio) * 1 - damageReductionPercent;
            }
            else
            {
                finalDamage = baseDamage * 1 - damageReductionPercent;
            }
            if (finalDamage < 0)
            {
                finalDamage = 0;
            }
            return new Damage(attacker, target, finalDamage * (1 + damageBonusFactor), critical, hits);
        }
    }
}
