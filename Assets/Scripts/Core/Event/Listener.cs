using UnityEngine;
using System;

namespace AFKHero.Core.Event{
	/// <summary>
	/// Représente un listener de base pour l'EventDispatcher
	/// </summary>
	public class Listener<T> : IListener where T : GameEvent{

		/// <summary>
		/// La delegate à implémenter 
		/// </summary>
		public delegate void GameEventAction(ref T e);

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
		public Listener(GameEventAction callback){
			this.Callback = callback;
			this.Priority = 0;
		}

		/// <summary>
		/// Créé une instance à partir d'un callback et d'une priorité.
		/// </summary>
		/// <param name="callback">Callback.</param>
		/// <param name="priority">Priority.</param>
		public Listener(GameEventAction callback, int priority){
			this.Callback = callback;
			this.Priority = priority;
		}

		public Type getType(){
			return typeof(T);
		}

		public void Call(ref object e){
			T eventData = (T)e;
			this.Callback(ref eventData);
		}

		public int getPriority(){
			return this.Priority;
		}
	}
}