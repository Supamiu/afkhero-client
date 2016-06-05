using UnityEngine;
using System.Collections;
using Spine.Unity;
using AFKHero.Core.Event;

namespace AFKHero.Common{
	public class AttackOnCollide : MonoBehaviour {

		void OnCollisionEnter2D(Collision2D coll){
			EventDispatcher.Instance.dispatch("debug", new GenericGameEvent<string>("J'vais lui péter la cheutron !"));
		}
	}
}
