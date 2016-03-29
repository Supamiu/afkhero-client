using UnityEngine;
using System.Collections;
using System;

namespace AFKHero.Core.Event{
	public class Listener : MonoBehaviour{

		public delegate void GameEventAction<T>(ref T e) where T : GameEvent;

		public int Priority{ get; private set; }

		public GameEventAction<GameEvent> Callback { get; private set; }

		public Listener(GameEventAction<GameEvent> callback){
			this.Callback = callback;
			this.Priority = 0;
		}

		public Listener(GameEventAction<GameEvent> callback, int priority){
			this.Callback = callback;
			this.Priority = priority;
		}
	}
}
