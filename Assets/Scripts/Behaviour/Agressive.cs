using UnityEngine;
using System.Collections;
using Spine.Unity;
using AFKHero.Core.Event;

namespace AFKHero.Behaviour{
	public class Agressive : MonoBehaviour {

		private SkeletonAnimation anim;

		//private Damageable target;

		void Start(){
			this.anim = GetComponent<SkeletonAnimation> ();
			this.anim.state.Complete += (Spine.AnimationState state, int trackIndex, int loopCount) => {
				if(trackIndex == 10){
					this.attack();
				}
			};
		}

		void OnCollisionEnter2D(Collision2D coll){
			this.anim.state.SetAnimation (10, "Attack", true);
			//this.target = coll.gameObject.GetComponent<Damageable> ();
		}

		private void attack(){
			
		}
	}
}
