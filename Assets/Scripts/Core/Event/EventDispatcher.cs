using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AFKHero.Core.Tools;

namespace AFKHero.Core.Event{

	/// <summary>
	/// Event dispatcher.
	/// Permet de gérer la propagation des events par référence avec une priorité
	/// </summary>
	public class EventDispatcher : Singleton<EventDispatcher> {

		//Pour éviter qu'une instance ne soit faite. 
		protected EventDispatcher() {}

		public delegate void GenericEventHandler<T>(T e) where T : GameEvent;

		private Dictionary<string, List<Listener>> registrations = new Dictionary<string, List<Listener>>();

		/// <summary>
		/// Ajoute le listener sur le type donné avec sa priorité.
		/// </summary>
		/// <param name="type">Le type d'event</param>
		/// <param name="listener">Le listener à ajouter</param>
		public void register(string type, Listener listener){
			if (!this.registrations.ContainsKey (type)) {
				this.registrations.Add (type, new List<Listener> ());
			}
			this.registrations[type].Add(listener);
		}

		/// <summary>
		/// Permet d'enregistrer un subscriber sur l'eventDispatcher.
		/// </summary>
		/// <param name="subscriber">Subscriber.</param>
		public void subscribe(Subscriber subscriber){
			foreach (string key in subscriber.getSubscribedEvents().Keys) {
				List<Listener> ls;
				subscriber.getSubscribedEvents ().TryGetValue (key, out ls);
				foreach (Listener l in ls) {
					this.register (key, l);
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
		public GameEvent dispatch(string type, GameEvent eventData){
			List<Listener> ls;
			this.registrations.TryGetValue (type, out ls);
			ls.Sort ((x, y) => y.Priority - x.Priority);
			foreach(Listener l in ls){
				l.Callback(ref eventData);
				if (eventData.isPropagationStopped ()) {
					break;
				}
			}
			return eventData;
		}
	}
}