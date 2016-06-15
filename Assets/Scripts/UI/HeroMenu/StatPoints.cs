using UnityEngine;
using System.Collections;
using AFKHero.Behaviour.Hero;
using AFKHero.Core.Event;
using UnityEngine.UI;

namespace AFKHero.UI
{
	[RequireComponent(typeof(Text))]
	public class StatPoints : MonoBehaviour
	{
		private Text text;

		void Start(){
			this.text = GetComponent<Text> ();
			EventDispatcher.Instance.Register ("stat.points.updated", new Listener<GenericGameEvent<int>> ((ref GenericGameEvent<int> e) => {
				this.text.text = e.Data.ToString();
			}));
		}
	}
}
