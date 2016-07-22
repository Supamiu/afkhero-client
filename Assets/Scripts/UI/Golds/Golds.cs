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
		void Start ()
		{
            display = GetComponent<Text> ();
			EventDispatcher.Instance.Register ("ui.gold", new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
                display.text = Formatter.Format(e.Data);
			}));
			EventDispatcher.Instance.Register ("gold", new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
				GoldGain gain = Instantiate(gainGoldTextPrefab);
				gain.transform.SetParent(transform, false);
				gain.transform.position = transform.position;
				gain.SetAmount(e.Data);
			}));
		}
	}
}
