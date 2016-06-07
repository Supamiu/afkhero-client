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

		void Start(){
			this.str = GetComponent<Strength> ();
			this.anim = GetComponent<SkeletonAnimation> ();
			this.anim.state.Complete += (Spine.AnimationState state, int trackIndex, int loopCount) => {
				if(trackIndex == 10){
					EventDispatcher.Instance.dispatch ("attack", new GenericGameEvent<Attack>(new Attack (this.str.Value, this.target, this)));
				}
			};
		}

		void OnCollisionEnter2D(Collision2D coll){
			this.target = coll.gameObject.GetComponent<Damageable> ();
			this.anim.state.SetAnimation (10, "Attack", true);
		}
	}
}
