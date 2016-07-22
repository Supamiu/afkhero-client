using UnityEngine;
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
                gold += e.Data;
				EventDispatcher.Instance.Dispatch ("ui.gold", new GenericGameEvent<double> (gold));
			}));
		}

		public SaveData Save (SaveData data)
		{
			data.gold = gold;
			return data;
		}

		public void Load (SaveData data)
		{
            gold = data.gold;
			EventDispatcher.Instance.Dispatch ("ui.gold", new GenericGameEvent<double> (gold));
		}
	}
}