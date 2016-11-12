using UnityEngine;
using AFKHero.Core.Event;


namespace AFKHero.Behaviour
{
    [RequireComponent (typeof(BoxCollider2D))]
	public class StopScrollOnEncounter : MonoBehaviour
	{
	    private void OnCollisionEnter2D (Collision2D coll)
		{
			EventDispatcher.Instance.Dispatch (Events.Movement.ENABLED, new GenericGameEvent<bool> (false));
			var target = coll.gameObject.GetComponent<Damageable> ();
			if (target != null) {
				target.onDeath += OnTargetDeath;
			}
		}

	    private void OnCollisionExit2D (Collision2D coll)
		{
			EventDispatcher.Instance.Dispatch (Events.Movement.ENABLED, new GenericGameEvent<bool> (true));
		}

		public void OnTargetDeath(){
			EventDispatcher.Instance.Dispatch (Events.Movement.ENABLED, new GenericGameEvent<bool> (true));
		}
	}
}
