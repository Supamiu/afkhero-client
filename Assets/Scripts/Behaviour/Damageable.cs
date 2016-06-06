using UnityEngine;
using System.Collections;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Behaviour{
	[RequireComponent(typeof(Vitality))]	
	public class Damageable : MonoBehaviour {
		
		private Vitality vitality;

		void Start(){
			this.vitality = GetComponent<Vitality> ();
			EventDispatcher.Instance.register ("attack", new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> gameEvent) => {
				if(gameEvent.Data.target == this){
					this.vitality.currentHp -= gameEvent.Data.getDamage();
				}
			}, 0));
		}

		void Damage(double amount){
			this.vitality.currentHp -= amount;
		}
	}
}
