using UnityEngine;
using UnityEngine.UI;
using AFKHero.Core.Event;
using AFKHero.Common;

namespace AFKHero.UI
{
    [RequireComponent (typeof(Text))]
	public class Distance : MonoBehaviour
	{
		private Text text;

		private float distance;
		// Use this for initialization
	    private void Start ()
		{
            text = GetComponent<Text> ();
			EventDispatcher.Instance.Register (Events.Movement.MOVED, new Listener<GenericGameEvent<float>> ((ref GenericGameEvent<float> e) => {
                distance = AFKHero.GetDistance();
                text.text = Formatter.ToDistanceString (distance);
			}));
		}
	}
}
