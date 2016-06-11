using UnityEngine;
using System.Collections;
using AFKHero.Tools;

namespace AFKHero.Behaviour
{
	public class Spawnable : MonoBehaviour, Ponderable
	{

		[Header("Spawn ponderation")]
		public int weight = 1;

		[Header ("Ratios")]
		public float vitalityRatio = 1f;

		public float strengthRatio = 1f;

		public int GetWeight(){
			return this.weight;
		}
	}
}
