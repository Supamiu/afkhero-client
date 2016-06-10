using UnityEngine;
using System.Collections;
using Spine.Unity;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Stat;
using Spine;

namespace AFKHero.Behaviour
{
	
	[RequireComponent (typeof(Strength))]
	public class Agressive : MonoBehaviour
	{

		private SkeletonAnimation anim;

		private Damageable target;

		public Strength Strength { get; private set;}

		private Damage nextDamage;

		[Header ("Animations")]
		[SpineEvent]
		public string hitEvent = "Hit";

		[SpineAnimation (dataField: "skeletonAnimation")]
		public string walkName = "Walk";

		[SpineAnimation (dataField: "skeletonAnimation")]
		public string attackName = "Attack";

		[SpineAnimation (dataField: "skeletonAnimation")]
		public string afterKillName = "Idle";

		void Start ()
		{
			this.Strength = GetComponent<Strength> ();
			this.anim = GetComponent<SkeletonAnimation> ();
			this.anim.state.Event += (Spine.AnimationState state, int trackIndex, Spine.Event e) => {
				if (this.target != null && e.Data.Name == this.hitEvent && state.GetCurrent (trackIndex).Animation.Name == attackName) {
					EventDispatcher.Instance.Dispatch ("attack.damage", new GenericGameEvent<Damage> (this.nextDamage));
				}
			};
			//Avant le premier coup, on compute.
			this.anim.state.Start += (Spine.AnimationState state, int trackIndex) => {				
				if(state.GetCurrent(trackIndex).Animation.Name == this.attackName){
					this.ComputeDamage();
				}
			};
			//Après chaque coup, on compute le coup suivant.
			this.anim.state.Complete += (Spine.AnimationState state, int trackIndex, int loopCount) => {
				if (state.GetCurrent (trackIndex).Animation.Name == this.attackName) {
					this.ComputeDamage ();
				}
			};
		}

		void OnCollisionEnter2D (Collision2D coll)
		{
			Damageable collider = coll.gameObject.GetComponent<Damageable> ();
			if (collider != null) {
				this.target = collider;
				this.target.onDeath += OnTargetDeath;
				this.anim.AnimationName = attackName;
			}
		}

		void OnTargetDeath ()
		{
			this.target = null;
			this.anim.AnimationName = this.afterKillName;
		}

		void ComputeDamage(){
			this.nextDamage = ((GenericGameEvent<Attack>)EventDispatcher.Instance.Dispatch ("attack.compute", new GenericGameEvent<Attack>(new Attack(this, this.target)))).Data.getDamage();
		}
	}
}
