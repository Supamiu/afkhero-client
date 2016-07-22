using UnityEngine;
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
            image = GetComponent<Image> ();
			EventDispatcher.Instance.Register ("experience.ui", new Listener<GenericGameEvent<XPGain>>((ref GenericGameEvent<XPGain> e) => {
                image.fillAmount = (float) (e.Data.xp / e.Data.xpForNextLevel);
			}));
		}
	}
}
