using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;
using System.Linq;

namespace AFKHero.Behaviour
{
	[RequireComponent (typeof(Vitality))]	
	public class Damageable : MonoBehaviour
	{

		public bool isMortal = true;

		private Vitality vitality;

		public delegate void DeathEvent ();

		public event DeathEvent onDeath;

		private IListener listener;

		void Start ()
		{
			this.vitality = GetComponent<Vitality> ();
			this.listener = new Listener<GenericGameEvent<Attack>> ((ref GenericGameEvent<Attack> gameEvent) => {
				if (gameEvent.Data.target == this) {
					this.Damage (gameEvent.Data.getDamage ());
				}
			}, 0);
			EventDispatcher.Instance.register ("attack", this.listener);
		}

		void Damage (double amount)
		{
			this.vitality.currentHp -= amount;
			if (this.vitality.currentHp <= 0 && this.isMortal) {
				this.Die ();
			}
		}

		void Die ()
		{
			IEnumerable<IOnDeath> deathListeners = this.gameObject.GetComponents<Component> ().OfType<IOnDeath> ();
			foreach (IOnDeath listener in deathListeners) {
				listener.OnDeath ();
			}
			EventDispatcher.Instance.unregister ("attack", this.listener);
			if (this.onDeath != null) {
				this.onDeath ();
			}
			Destroy (this.gameObject);
		}
	}
}
