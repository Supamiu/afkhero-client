using UnityEngine;
using System.Collections;
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
			this.text = GetComponent<Text> ();
			this.listener = new Listener<GenericGameEvent<LevelUp>>((ref GenericGameEvent<LevelUp> e) => {
				this.text.text = "Lvl "+e.Data.level;
			});
			EventDispatcher.Instance.Register ("level.up", this.listener);
			EventDispatcher.Instance.Register ("level.update", this.listener);
		}
	}
}

