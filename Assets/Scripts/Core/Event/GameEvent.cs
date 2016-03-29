using UnityEngine;
using System.Collections;

namespace AFKHero.Core.Event{
	public class GameEvent {

		private bool propagationStopped = false;

		public bool isPropagationStopped(){
			return this.propagationStopped;
		}

		public void stopPropagation(){
			this.propagationStopped = true;
		}
	}
}
