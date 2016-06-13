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
		public float xpRatio = 1f;

		public double GetGold ()
		{
			return Math.Round (this.xpRatio * GetComponent<Spawnable> ().Distance);
		}

		public void OnDeath ()
		{
			EventDispatcher.Instance.Dispatch ("gold", new GenericGameEvent<double> (this.GetGold ()));
		}
	}
}
