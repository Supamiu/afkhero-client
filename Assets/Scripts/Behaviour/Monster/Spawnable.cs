using UnityEngine;
using AFKHero.Tools;
using AFKHero.Stat;

namespace AFKHero.Behaviour.Monster
{
    [RequireComponent (typeof(Strength))]
	[RequireComponent (typeof(Vitality))]
	[RequireComponent (typeof(Dodge))]
	[RequireComponent (typeof(Defense))]
    public class Spawnable : MonoBehaviour, Ponderable
	{

		[Header ("Spawn ponderation")]
		public int weight = 10;

		[Header ("Ratio vitality/m")]
		public float vitalityRatio = 1f;

		[Header ("Ratio strength/m")]
		public float strengthRatio = 1f;

		[Header ("Ratio dodge/m")]
		public float dodgeRatio = 1f;

        [Header("Valeur de base de la défense")]
        public double baseDefenseValue = 1f;

		public float Distance{ get; private set; }

		public int GetWeight ()
		{
			return weight;
		}

		public Spawnable Init (float distance)
		{
            Distance = distance;
            GetComponent<Strength>().amount = RatioEngine.Instance.GetEnemyDamage (strengthRatio, distance);
            GetComponent<Vitality>().amount = RatioEngine.Instance.GetEnemyHealth (vitalityRatio, distance);
            GetComponent<Dodge>().amount = RatioEngine.Instance.GetEnemyDodge (dodgeRatio, distance);
            GetComponent<Defense>().amount = RatioEngine.Instance.GetEnemyDefense (baseDefenseValue, distance);
            return this;
		}
	}
}
