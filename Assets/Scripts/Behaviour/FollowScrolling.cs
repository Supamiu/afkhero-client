using UnityEngine;
using System.Collections;
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
			this.movement = GetComponent<AutoMoveAndRotate> ();
			this.listener = new Listener<GenericGameEvent<bool>>((ref GenericGameEvent<bool> e) => {
				this.movement.enabled = e.Data;
			}, 10);
				EventDispatcher.Instance.Register("movement.enabled", this.listener);
		}

		public void OnDeath(){
			EventDispatcher.Instance.Unregister ("movement.enabled", this.listener);
		}
	}
}
