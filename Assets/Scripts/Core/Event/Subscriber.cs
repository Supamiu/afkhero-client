using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AFKHero.Core.Event{

	/// <summary>
	/// Permet de définir un "connecteur" sur l'eventDispatcher.
	/// </summary>
	public interface Subscriber {

		Dictionary<string, List<Listener>> getSubscribedEvents();
	}
}
