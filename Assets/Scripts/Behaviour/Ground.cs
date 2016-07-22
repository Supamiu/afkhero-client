using UnityEngine;
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
            scrolling = GetComponent<ScrollingScript> ();
		}

		void FixedUpdate(){
            time += Time.fixedDeltaTime;
			if (time >= tickInterval) {
                Tick();
                time = 0f;
			}
		}

		void Tick() {
			if (scrolling.moving) {
				EventDispatcher.Instance.Dispatch("movement.moved", new GenericGameEvent<float>(scrolling.speed.x/4));
			}
		}
	}
}
