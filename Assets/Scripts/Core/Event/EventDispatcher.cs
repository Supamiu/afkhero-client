using System.Collections.Generic;
using AFKHero.Core.Tools;

namespace AFKHero.Core.Event
{

    /// <summary>
    /// Event dispatcher.
    /// Permet de gérer la propagation des events par référence avec une priorité
    /// </summary>
    public class EventDispatcher : Singleton<EventDispatcher>
	{

		//Pour éviter qu'une instance ne soit faite.
		protected EventDispatcher ()
		{
		}

		public delegate void GenericEventHandler<T> (T e) where T : GameEvent;

		private Dictionary<string, List<IListener>> registrations = new Dictionary<string, List<IListener>> ();

		/// <summary>
		/// Ajoute le listener sur le type donné avec sa priorité.
		/// </summary>
		/// <param name="type">Le type d'event</param>
		/// <param name="listener">Le listener à ajouter</param>
		public void Register (string type, IListener listener)
		{
			if (listener == null) {
				return;
			}
			if (!registrations.ContainsKey (type)) {
                registrations.Add (type, new List<IListener> ());
			}
            registrations[type].Add (listener);
		}

		/// <summary>
		/// Retire le listener donné d'une liste d'event.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="listener">Listener.</param>
		public void Unregister (string type, IListener listener)
		{
			if (!registrations.ContainsKey (type)) {
				return;
			}
			List<IListener> listeners = registrations[type];
			listeners.Remove (listener);
            registrations[type] = listeners;
		}

		/// <summary>
		/// Permet d'enregistrer un subscriber sur l'eventDispatcher.
		/// </summary>
		/// <param name="subscriber">Subscriber.</param>
		public void Subscribe (Subscriber subscriber)
		{
			foreach (string key in subscriber.getSubscribedEvents().Keys) {
				List<IListener> ls;
				subscriber.getSubscribedEvents ().TryGetValue (key, out ls);
				foreach (IListener l in ls) {
                    Register(key, l);
				}
			}
		}

		/// <summary>
		/// Propage l'event en vérifiant qu'il soit toujours à propager.
		/// Avant la propagation, les listeners sont ordonnés par priorité.
		/// Si un listener est ajouté pendant la propagation, il sera 
		/// ignoré pour cette propagation et ne sera pris en compte qu'à la suivante.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="eventData">Event data.</param>
		public GameEvent Dispatch (string type, GameEvent eventData)
		{
			List<IListener> ls;
			object e = (object)eventData;
            registrations.TryGetValue (type, out ls);
			if (ls != null) {
				List<IListener> tmp = new List<IListener> (ls);
				ls.Sort ((x, y) => y.getPriority () - x.getPriority ());
				foreach (IListener l in tmp) {
					l.Call (ref e);
					if (eventData.isPropagationStopped ()) {
						break;
					}
				}
			} else {
				//Debug.LogWarning ("Event "+type+" called with no listeners.");
			}
			return eventData;
		}

		/// <summary>
		/// Dispatch the specified type.
		/// </summary>
		/// <param name="type">Type.</param>
		public GameEvent Dispatch (string type)
		{
			GameEvent eventData = new GameEvent ();
			List<IListener> ls;
			object e = (object)eventData;
            registrations.TryGetValue (type, out ls);
			if (ls != null) {
				List<IListener> tmp = new List<IListener> (ls);
				ls.Sort ((x, y) => y.getPriority () - x.getPriority ());
				foreach (IListener l in tmp) {
					l.Call (ref e);
					if (eventData.isPropagationStopped ()) {
						break;
					}
				}
			} else {
				//Debug.LogWarning ("Event "+type+" called with no listeners.");
			}
			return eventData;
		}


		/// <summary>
		/// Reset entièrement l'EventManager.
		/// </summary>
		public void Clear(){
            registrations = new Dictionary<string, List<IListener>> ();
		}
	}
}