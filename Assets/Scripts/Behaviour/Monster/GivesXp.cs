using UnityEngine;
using AFKHero.Core.Event;
using System;

namespace AFKHero.Behaviour.Monster
{
    [RequireComponent(typeof(Spawnable))]
	public class GivesXp : MonoBehaviour, IOnDeath
	{
		[Header ("Ratio xp/distance")]
		public float xpRatio = 1f;

		public double GetXp ()
		{
			return Math.Round(xpRatio * GetComponent<Spawnable>().Distance);
		}

		public void OnDeath(){
			EventDispatcher.Instance.Dispatch ("experience", new GenericGameEvent<double> (GetXp()));
		}
	}
}
