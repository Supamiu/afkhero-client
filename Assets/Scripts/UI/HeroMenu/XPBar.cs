using UnityEngine;
using System.Collections;
using AFKHero.Core.Event;
using AFKHero.EventData;
using UnityEngine.UI;

namespace AFKHero.UI.HeroMenu
{
	[RequireComponent(typeof(Image))]
	public class XPBar : MonoBehaviour
	{
		private Image image;

		// Use this for initialization
		void Start ()
		{
			this.image = GetComponent<Image> ();
			EventDispatcher.Instance.Register ("experience.ui", new Listener<GenericGameEvent<XPGain>>((ref GenericGameEvent<XPGain> e) => {
				this.image.fillAmount = (float) (e.Data.xp / e.Data.xpForNextLevel);
			}));
		}
	}
}
