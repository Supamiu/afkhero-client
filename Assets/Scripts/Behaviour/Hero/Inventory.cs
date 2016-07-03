using UnityEngine;
using System.Collections;
using AFKHero.Core.Save;
using AFKHero.Core.Event;

namespace AFKHero.Behaviour
{
	public class Inventory : MonoBehaviour, Saveable
	{
		private double gold = 0;

		void Start ()
		{
			EventDispatcher.Instance.Register ("gold", new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
				this.gold += e.Data;
				EventDispatcher.Instance.Dispatch ("ui.gold", new GenericGameEvent<double> (this.gold));
			}));
		}

		public SaveData Save (SaveData data)
		{
			data.gold = this.gold;
			return data;
		}

		public void Load (SaveData data)
		{
			this.gold = data.gold;
			EventDispatcher.Instance.Dispatch ("ui.gold", new GenericGameEvent<double> (this.gold));
		}
	}
}