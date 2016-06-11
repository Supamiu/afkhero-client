using UnityEngine;
using System.Collections;
using AFKHero.Core.Event;

namespace AFKHero.Common{
	public class Debugger : MonoBehaviour {
		
		void Start () {
			Listener<GenericGameEvent<string>> listener = new Listener<GenericGameEvent<string>> ((ref GenericGameEvent<string> e) => {
				Debug.Log(e.Data);
			});
			EventDispatcher.Instance.Register ("debug", listener);
			Debug.Log ("Debugger started");
		}
	}
}

