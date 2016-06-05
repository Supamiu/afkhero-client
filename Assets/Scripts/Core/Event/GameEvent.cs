﻿using UnityEngine;
using System.Collections;

namespace AFKHero.Core.Event{
	/// <summary>
	/// Un event de base sans données.
	/// </summary>
	public class GameEvent {

		/// <summary>
		/// Indique si la propagation a été stoppée ou non, utile pour le dispatcher.
		/// </summary>
		private bool propagationStopped = false;

		/// <summary>
		/// Indique si la propagation a été stoppée ou non.
		/// </summary>
		public bool isPropagationStopped(){
			return this.propagationStopped;
		}

		/// <summary>
		/// Stop la propagation.
		/// </summary>
		public void stopPropagation(){
			this.propagationStopped = true;
		}
	}
}
