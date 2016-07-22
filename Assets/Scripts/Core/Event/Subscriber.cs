using System.Collections.Generic;

namespace AFKHero.Core.Event
{

    /// <summary>
    /// Permet de définir un "connecteur" sur l'eventDispatcher.
    /// </summary>
    public interface Subscriber {

		/// <summary>
		/// Récupère la liste des enregistremens du subscriber.
		/// </summary>
		/// <returns>The subscribed events.</returns>
		Dictionary<string, List<IListener>> getSubscribedEvents();
	}
}
