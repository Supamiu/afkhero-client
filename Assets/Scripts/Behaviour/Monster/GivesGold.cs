using UnityEngine;
using System;
using AFKHero.Core.Event;

namespace AFKHero.Behaviour.Monster
{
    public class GivesGold : MonoBehaviour, IOnDeath
	{
	
		[Header ("Ratio gold/distance")]
		public float goldRatio = 1f;

		public double GetGold ()
		{
			return Math.Round (goldRatio * GetComponent<Spawnable> ().Distance);
		}

		public void OnDeath ()
		{
			EventDispatcher.Instance.Dispatch ("gold", new GenericGameEvent<double> (GetGold()));
		}
	}
}
