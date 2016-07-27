using UnityEngine;
using AFKHero.Tools;
using AFKHero.Stat;
using System.Collections.Generic;
using AFKHero.Model;
using System;
using AFKHero.Core;

namespace AFKHero.Behaviour.Monster
{
    [RequireComponent (typeof(Strength))]
	[RequireComponent (typeof(Vitality))]
	[RequireComponent (typeof(Dodge))]
	[RequireComponent (typeof(Defense))]
    [Serializable]
    public class Spawnable : MonoBehaviour, Ponderable, IOnDeath
	{

		[Header ("Spawn ponderation")]
		public int weight;

		[Header ("Ratio vitality/m")]
		public float vitalityRatio;

		[Header ("Ratio strength/m")]
		public float strengthRatio;

		[Header ("Ratio dodge/m")]
		public float dodgeRatio;

        [Header("Valeur de base de la défense")]
        public double baseDefenseValue;

        public List<Drop> dropList;

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

        public void OnDeath()
        {
            DropEngine.Instance.Drop();
        }
    }
}
