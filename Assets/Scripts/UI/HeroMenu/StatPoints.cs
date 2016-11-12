using UnityEngine;
using AFKHero.Core.Event;
using UnityEngine.UI;

namespace AFKHero.UI
{
    [RequireComponent(typeof(Text))]
	public class StatPoints : MonoBehaviour
	{
		private Text text;

	    private void Awake(){
            text = GetComponent<Text> ();
			EventDispatcher.Instance.Register (Events.Stat.Points.UPDATED, new Listener<GenericGameEvent<int>> ((ref GenericGameEvent<int> e) => {
                text.text = e.Data.ToString();
			}));
		}
	}
}
