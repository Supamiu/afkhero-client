using UnityEngine;
using System.Collections;

namespace AFKHero.Core.Event{
	/// <summary>
	/// Event générique qui est un GameEvent + des datas de type T.
	/// </summary>
	public class GenericGameEvent<T> : GameEvent {

		/// <summary>
		/// Les datas contenus dans l'event.
		/// </summary>
		/// <value>The data.</value>
		private T Data { get; set; }
	}
}
