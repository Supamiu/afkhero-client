using UnityEngine;
using AFKHero.Core.Event;
using UnityStandardAssets.Utility;

namespace AFKHero.Behaviour
{
    [RequireComponent(typeof(AutoMoveAndRotate))]
	public class FollowScrolling : MonoBehaviour, IOnDeath
	{
		AutoMoveAndRotate movement;
		IListener listener;

		// Use this for initialization
		void Start ()
		{
            movement = GetComponent<AutoMoveAndRotate> ();
            listener = new Listener<GenericGameEvent<bool>>((ref GenericGameEvent<bool> e) => {
                movement.enabled = e.Data;
			}, 10);
				EventDispatcher.Instance.Register("movement.enabled", listener);
		}

		public void OnDeath(){
			EventDispatcher.Instance.Unregister ("movement.enabled", listener);
		}
	}
}
