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
		void Awake ()
		{
            text = GetComponent<Text> ();
            listener = new Listener<GenericGameEvent<LevelUp>>((ref GenericGameEvent<LevelUp> e) => {
                text.text = "Lvl "+e.Data.level;
			});
			EventDispatcher.Instance.Register ("level.up", listener);
			EventDispatcher.Instance.Register ("level.update", listener);
		}
	}
}

