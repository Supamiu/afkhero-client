using UnityEngine;
using System.Collections;
using AFKHero.Core.Save;
using AFKHero.Core.Event;

namespace AFKHero.Behaviour
{
	public class Inventory : MonoBehaviour, Saveable
	{
		private double gold = 0;

		void Start(){
			EventDispatcher.Instance.Register ("gold", new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
				this.gold += e.Data;
				EventDispatcher.Instance.Dispatch ("ui.gold", new GenericGameEvent<double> (this.gold));
			}));
		}

		public string GetIdentifier(){
			return "inventory";
		}

		public object[] Save ()
		{
			object[] data = { this.gold };
			return data;
		}

		public void Load (object[] data)
		{
			this.gold = (double)data [0];
		}
	}
}