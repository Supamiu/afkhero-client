using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
			this.display = GetComponent<Text> ();
			EventDispatcher.Instance.Register ("ui.gold", new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
				this.display.text = Formatter.Format(e.Data);
			}));
			EventDispatcher.Instance.Register ("gold", new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
				GoldGain gain = Instantiate(gainGoldTextPrefab);
				gain.transform.SetParent(this.transform, false);
				gain.transform.position = this.transform.position;
				gain.SetAmount(e.Data);
			}));
		}
	}
}
