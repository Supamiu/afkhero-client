using UnityEngine;
using AFKHero.Core.Event;
using UnityEngine.UI;
using AFKHero.EventData;

namespace AFKHero.UI.HeroMenu
{
    [RequireComponent(typeof(Text))]
	public class Level : MonoBehaviour
	{

		private Text text;

		private IListener listener;

		// Use this for initialization
	    private void Awake ()
		{
            text = GetComponent<Text> ();
            listener = new Listener<GenericGameEvent<LevelUp>>((ref GenericGameEvent<LevelUp> e) => {
                text.text = "Lvl "+e.Data.level;
			});
			EventDispatcher.Instance.Register (Events.Level.UP, listener);
			EventDispatcher.Instance.Register (Events.Level.UPDATE, listener);
		}
	}
}

