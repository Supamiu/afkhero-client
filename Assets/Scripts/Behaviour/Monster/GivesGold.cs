using UnityEngine;
using System.Collections;
using AFKHero.Behaviour;
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
			return Math.Round (this.goldRatio * GetComponent<Spawnable> ().Distance);
		}

		public void OnDeath ()
		{
			EventDispatcher.Instance.Dispatch ("gold", new GenericGameEvent<double> (this.GetGold ()));
		}
	}
}
