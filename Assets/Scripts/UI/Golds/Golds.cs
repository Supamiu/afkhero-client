using UnityEngine;
using UnityEngine.UI;
using AFKHero.Core.Event;
using AFKHero.Common;

namespace AFKHero.UI.Golds
{
    [RequireComponent(typeof(Text))]
	public class Golds : MonoBehaviour
	{
		private Text display;

		public GoldGain gainGoldTextPrefab;

		// Use this for initialization
	    private void Start ()
		{
            display = GetComponent<Text> ();
			EventDispatcher.Instance.Register (Events.UI.GOLD_UPDATED, new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
                display.text = Formatter.Format(e.Data);
			}));
			EventDispatcher.Instance.Register (Events.GOLD_GAIN, new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
				var gain = Instantiate(gainGoldTextPrefab);
				gain.transform.SetParent(transform, false);
				gain.transform.position = transform.position;
				gain.SetAmount(e.Data);
			}));
		}
	}
}
