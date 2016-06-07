using UnityEngine;
using System.Collections;
using Spine.Unity;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Stat;

namespace AFKHero.Behaviour{
	
	[RequireComponent(typeof(Strength))]
	public class Agressive : MonoBehaviour {

		private SkeletonAnimation anim;

		private Damageable target;

		private Strength str;

		[Header("Pour définir l'animation après un kill (Idle ou Walk)")]
		public bool moveAfterKill = false;

		void Start(){
			this.str = GetComponent<Strength> ();
			this.anim = GetComponent<SkeletonAnimation> ();
			this.anim.state.Complete += (Spine.AnimationState state, int trackIndex, int loopCount) => {
				if(state.GetCurrent(trackIndex).Animation.Name == "Attack"){
					EventDispatcher.Instance.dispatch ("attack", new GenericGameEvent<Attack>(new Attack (this.str.Value, this.target, this)));
				}
			};
		}

		void OnCollisionEnter2D(Collision2D coll){
			this.target = coll.gameObject.GetComponent<Damageable> ();
			this.target.onDeath += OnTargetDeath;
			this.anim.AnimationName = "Attack";
		}

		void OnTargetDeath(){
			this.target = null;
			string animation = moveAfterKill ? "Walk" : "Idle";
			this.anim.AnimationName = animation;
		}
	}
}
