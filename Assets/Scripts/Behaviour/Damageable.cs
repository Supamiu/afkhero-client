using UnityEngine;
using System.Collections.Generic;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;
using System.Linq;
using Spine.Unity;
using System;

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
            anim = GetComponent<SkeletonAnimation> ();
            vitality = GetComponent<Vitality> ();
            listener = new Listener<GenericGameEvent<Damage>> ((ref GenericGameEvent<Damage> gameEvent) => {
				if (gameEvent.Data.target == this) {
					if (gameEvent.Data.hits) {
                        Damage(gameEvent.Data.damage);
					}
				}
			}, 0);
			EventDispatcher.Instance.Register ("attack.damage", listener);
            anim.state.Complete += (Spine.AnimationState state, int trackIndex, int loopCount) => {
				if (state.GetCurrent (trackIndex).Animation.Name == deathAnimation) {
                    Die();
				}
			};
		}

		void Damage (double amount)
		{
			amount = Math.Round (amount);
            vitality.currentHp -= amount;
			if (vitality.currentHp <= 0 && isMortal) {
                anim.loop = false;
                anim.AnimationName = deathAnimation;
			}
			if (OnDamaged != null) {
                OnDamaged.Invoke();
			}
		}

		void Die ()
		{
			IEnumerable<IOnDeath> deathListeners = gameObject.GetComponents<Component> ().OfType<IOnDeath> ();
			foreach (IOnDeath dlistener in deathListeners) {
				dlistener.OnDeath ();
			}
			EventDispatcher.Instance.Unregister ("attack.damage", listener);
			if (onDeath != null) {
                onDeath.Invoke();
			}
			if (destroyOnDeath) {
				Destroy (gameObject);
			}
		}
	}
}
