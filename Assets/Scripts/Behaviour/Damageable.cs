using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;
using System.Linq;
using Spine.Unity;
using System;

using AFKHero.Common;

namespace AFKHero.Behaviour
{
	[RequireComponent (typeof(Vitality))]
	public class Damageable : MonoBehaviour
	{
		public delegate void Damaged();

		public event Damaged OnDamaged;

		public bool isMortal = true;

		private Vitality vitality;

		public delegate void DeathEvent ();

		public event DeathEvent onDeath;

		private SkeletonAnimation anim;

		private IListener listener;

		[SpineAnimation (dataField: "skeletonAnimation")]
		public string deathAnimation = "Die";

		[Header ("Doit-on détruire le gameObject à sa mort physique?")]
		public bool destroyOnDeath = true;

		void Start ()
		{
			this.anim = GetComponent<SkeletonAnimation> ();
			this.vitality = GetComponent<Vitality> ();
			this.listener = new Listener<GenericGameEvent<Damage>> ((ref GenericGameEvent<Damage> gameEvent) => {
				if (gameEvent.Data.target == this) {
					if (gameEvent.Data.hits) {
						this.Damage (gameEvent.Data.damage);
					}
				}
			}, 0);
			EventDispatcher.Instance.Register ("attack.damage", this.listener);
			this.anim.state.Complete += (Spine.AnimationState state, int trackIndex, int loopCount) => {
				if (state.GetCurrent (trackIndex).Animation.Name == this.deathAnimation) {
					this.Die ();
				}
			};
		}

		void Damage (double amount)
		{
			amount = Math.Round (amount);
			this.vitality.currentHp -= amount;
			if (this.vitality.currentHp <= 0 && this.isMortal) {
				this.anim.loop = false;
				this.anim.AnimationName = this.deathAnimation;
			}
			if (this.OnDamaged != null) {
				this.OnDamaged();
			}
		}

		void Die ()
		{
			IEnumerable<IOnDeath> deathListeners = this.gameObject.GetComponents<Component> ().OfType<IOnDeath> ();
			foreach (IOnDeath listener in deathListeners) {
				listener.OnDeath ();
			}
			EventDispatcher.Instance.Unregister ("attack.damage", this.listener);
			if (this.onDeath != null) {
				this.onDeath ();
			}
			if (this.destroyOnDeath) {
				Destroy (this.gameObject);
			}
		}
	}
}
