using UnityEngine;
using System.Collections;

namespace AFKHero.Core.Event{
	public class GenericGameEvent<T> : GameEvent {

		private T data { get; set; }
	}
}
