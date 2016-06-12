using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AFKHero.Core.Event;
using AFKHero.Common;

namespace AFKHero.UI
{
	[RequireComponent (typeof(Text))]
	public class Distance : MonoBehaviour
	{
		private Text text;

		private float distance = 0;
		// Use this for initialization
		void Start ()
		{
			this.text = GetComponent<Text> ();
			EventDispatcher.Instance.Register ("movement.moved", new Listener<GenericGameEvent<float>> ((ref GenericGameEvent<float> e) => {
				this.distance = AFKHero.distance;
				this.text.text = Formatter.ToDistanceString (this.distance);
			}));
		}
	}
}
