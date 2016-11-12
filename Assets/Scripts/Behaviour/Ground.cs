using UnityEngine;
using AFKHero.Common;
using AFKHero.Core.Event;

namespace AFKHero.Behaviour
{
    [RequireComponent(typeof(ScrollingScript))]
	public class Ground : MonoBehaviour
	{
	    private ScrollingScript scrolling;

	    private float time;

	    private const float tickInterval = 0.5f;

	    // Use this for initialization
	    private void Start ()
		{
            scrolling = GetComponent<ScrollingScript> ();
		}

	    private void FixedUpdate(){
            time += Time.fixedDeltaTime;
	        if (!(time >= tickInterval)) return;
	        Tick();
	        time = 0f;
	    }

	    private void Tick() {
			if (scrolling.moving) {
				EventDispatcher.Instance.Dispatch(Events.Movement.MOVED, new GenericGameEvent<float>(scrolling.Speed/4f));
			}
		}
	}
}
