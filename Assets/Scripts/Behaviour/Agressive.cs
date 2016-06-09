using UnityEngine;
using System.Collections;
using Spine.Unity;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Stat;

namespace AFKHero.Behaviour
{
	
	[RequireComponent (typeof(Strength))]
	public class Agressive : MonoBehaviour
	{

		private SkeletonAnimation anim;

		private Damageable target;

		private Strength str;

		[Header ("Animations")]
		[SpineAnimation (dataField: "skeletonAnimation")]
		public string walkName = "Walk";

		[SpineAnimation (dataField: "skeletonAnimation")]
		public string attackName = "Attack";

		[SpineAnimation (dataField: "skeletonAnimation")]
		public string idleName = "Idle";

		[Header ("Pour définir l'animation après un kill (Idle ou Walk)")]
		public bool moveAfterKill = false;

		void Start ()
		{
			this.str = GetComponent<Strength> ();
			this.anim = GetComponent<SkeletonAnimation> ();
			this.anim.state.Event += (Spine.AnimationState state, int trackIndex, Spine.Event e) => {
				if (this.target != null && e.Data.Name == "Hit" && state.GetCurrent (trackIndex).Animation.Name == attackName) {
					EventDispatcher.Instance.dispatch ("attack", new GenericGameEvent<Attack> (new Attack (this.str.Value, this.target, this)));
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
			string animation = moveAfterKill ? walkName : idleName;
			this.anim.AnimationName = animation;
		}
	}
}
