using UnityEngine;
using System.Collections;
using AFKHero.Core.Event;


namespace AFKHero.Behaviour
{
	[RequireComponent (typeof(BoxCollider2D))]
	public class StopScrollOnEncounter : MonoBehaviour
	{
		void OnCollisionEnter2D (Collision2D coll)
		{
			EventDispatcher.Instance.dispatch ("movement.enabled", new GenericGameEvent<bool> (false));
			Damageable target = coll.gameObject.GetComponent<Damageable> ();
			if (target != null) {
				target.onDeath += OnTargetDeath;
			}
		}

		void OnCollisionExit2D (Collision2D coll)
		{
			EventDispatcher.Instance.dispatch ("movement.enabled", new GenericGameEvent<bool> (true));
		}

		public void OnTargetDeath(){
			EventDispatcher.Instance.dispatch ("movement.enabled", new GenericGameEvent<bool> (true));
		}
	}
}
