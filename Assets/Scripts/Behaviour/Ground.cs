using UnityEngine;
using System.Collections;
using AFKHero.Common;
using AFKHero.Core.Event;

namespace AFKHero.Behaviour
{
	[RequireComponent(typeof(ScrollingScript))]
	public class Ground : MonoBehaviour
	{

		ScrollingScript scrolling;

		float time = 0f;

		float tickInterval = 0.5f;

		// Use this for initialization
		void Start ()
		{
			this.scrolling = GetComponent<ScrollingScript> ();
		}

		void FixedUpdate(){
			this.time += Time.fixedDeltaTime;
			if (this.time >= this.tickInterval) {
				this.Tick ();
				this.time = 0f;
			}
		}

		void Tick() {
			if (scrolling.moving) {
				EventDispatcher.Instance.Dispatch("movement.moved", new GenericGameEvent<float>(scrolling.speed.x/2));
			}
		}
	}
}
