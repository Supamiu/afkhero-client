using UnityEngine;
using System.Collections;
using AFKHero.Tools;
using AFKHero.Stat;

namespace AFKHero.Behaviour.Monster
{
	[RequireComponent (typeof(Strength))]
	[RequireComponent (typeof(Vitality))]
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

		public float Distance{ get; private set; }

		public int GetWeight ()
		{
			return this.weight;
		}

		public Spawnable Init (float distance)
		{
			this.Distance = distance;
			this.GetComponent<Strength> ().amount = RatioEngine.Instance.GetEnemyDamage (this.strengthRatio, distance);
			this.GetComponent<Vitality> ().amount = RatioEngine.Instance.GetEnemyHealth (this.vitalityRatio, distance);
			this.GetComponent<Dodge> ().amount = RatioEngine.Instance.GetEnemyDodge (this.dodgeRatio, distance);
			return this;
		}
	}
}
