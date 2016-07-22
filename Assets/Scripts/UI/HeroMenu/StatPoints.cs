using UnityEngine;
using AFKHero.Core.Event;
using UnityEngine.UI;

namespace AFKHero.UI
{
    [RequireComponent(typeof(Text))]
	public class StatPoints : MonoBehaviour
	{
		private Text text;

		void Awake(){
            text = GetComponent<Text> ();
			EventDispatcher.Instance.Register ("stat.points.updated", new Listener<GenericGameEvent<int>> ((ref GenericGameEvent<int> e) => {
                text.text = e.Data.ToString();
			}));
		}
	}
}
