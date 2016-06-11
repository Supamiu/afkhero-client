using UnityEngine;
using System.Collections;
using AFKHero.Behaviour;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Model
{
	
	public class Weapon : MonoBehaviour
	{

		public Sprite sprite;

		public int attack = 1;

		public IListener listener;

		public bool isInit = false;

		private Agressive agressive;

		public void Init ()
		{
			this.listener = new Listener<GenericGameEvent<Attack>> ((ref GenericGameEvent<Attack> e) => {
				if(e.Data.attacker.name == this.agressive.name){
					e.Data.baseDamage *= this.attack;
				}
			}, 2000);
			this.isInit = true;
		}

		public void Attach (GameObject go)
		{
			Agressive agressive = go.GetComponent<Agressive> ();
			if (agressive == null) {
				Debug.LogError ("Weapon attached on non-agressive GameObject");
				return;
			}
			this.agressive = agressive;
			EventDispatcher.Instance.Register ("attack.compute", this.listener);
		}

		public void Detach ()
		{
			EventDispatcher.Instance.Unregister ("attack.compute", this.listener);
		}
	}
}
