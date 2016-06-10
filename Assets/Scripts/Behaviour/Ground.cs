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

		// Use this for initialization
		void Start ()
		{
			this.scrolling = GetComponent<ScrollingScript> ();
			InvokeRepeating ("Move", 0f, 0.1f);
		}

		void Move() {
			if (scrolling.moving) {
				EventDispatcher.Instance.Dispatch("movement.moved", new GenericGameEvent<float>(scrolling.speed.x/10f));
			}
		}
	}
}
