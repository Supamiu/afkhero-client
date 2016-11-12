using UnityEngine;
using System;

namespace AFKHero.Core.Event
{
	/// <summary>
	/// Représente un listener de base pour l'EventDispatcher
	/// </summary>
	public class Listener<T> : IListener where T : GameEvent
	{

        /// <summary>
        /// L'id unique du listener, peut servir.
        /// </summary>
        private readonly string id;

		/// <summary>
		/// La delegate à implémenter 
		/// </summary>
		public delegate void GameEventAction (ref T e);

		/// <summary>
		/// La priorité de l'event,
		/// </summary>
		/// <value>The priority.</value>
		public int Priority{ get; private set; }

		/// <summary>
		/// La callback appellée lors de l'event.
		/// </summary>
		public GameEventAction Callback { get; private set; }

		/// <summary>
		/// Créé une instance à partir d'une callback, la priorité est mise par défaut à 0.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public Listener (GameEventAction callback)
		{
            Callback = callback;
            Priority = 0;
		}

		/// <summary>
		/// Créé une instance à partir d'un callback et d'une priorité.
		/// </summary>
		/// <param name="callback">Callback.</param>
		/// <param name="priority">Priority.</param>
		public Listener (GameEventAction callback, int priority)
		{
            Callback = callback;
            Priority = priority;
		}

	    /// <summary>
	    /// Créé une instance à partir d'un callback, d'une priorité et d'un id.
	    /// </summary>
	    /// <param name="callback">Callback.</param>
	    /// <param name="priority">Priority.</param>
	    /// <param name="id">Id.</param>
	    public Listener(GameEventAction callback, int priority, string id)
        {
            Callback = callback;
            Priority = priority;
            this.id = id;
        }

        public Type getType ()
		{
			return typeof(T);
		}

		public void Call (ref object e)
		{
			try {
				var eventData = (T)e;
                Callback(ref eventData);
			} catch (InvalidCastException ex) {
				Debug.LogError (ex + " -> " + e + " could not be casted to destination type.");
			}
		}

		public int getPriority ()
		{
			return Priority;
		}

        public string GetId()
        {
            return id;
        }
    }
}
