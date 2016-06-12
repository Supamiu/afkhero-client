using UnityEngine;
using System.Collections;
using AFKHero.Core.Event;

namespace AFKHero.Behaviour.Monster
{
	[RequireComponent(typeof(Spawnable))]
	public class GivesXp : MonoBehaviour, IOnDeath
	{

		[Header ("Ratio xp/distance")]
		public float xpRatio = 1f;

		public double GetXp ()
		{
			return this.xpRatio * GetComponent<Spawnable>().Distance;
		}

		public void OnDeath(){
			EventDispatcher.Instance.Dispatch ("experience", new GenericGameEvent<double> (this.GetXp ()));
		}
	}
}
