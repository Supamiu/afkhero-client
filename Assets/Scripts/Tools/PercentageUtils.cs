using UnityEngine;
using System.Collections;
using AFKHero.Core.Tools;
using System.Collections.Generic;

namespace AFKHero.Tools
{
	public class PercentageUtils : Singleton<PercentageUtils>
	{
		//Pour éviter qu'une instance ne soit faite.
		protected PercentageUtils ()
		{
		}

		public bool GetResult (float percentage)
		{
			return percentage > Random.Range (0, 1f);
		}

		public T GetItemFromPonderables<T> (IEnumerable<T> items) where T : Ponderable
		{
			int total = 0;
			foreach (T item in items) {
				if (item.GetWeight () <= 0) {
					Debug.LogWarning ("Item with ponderation <= 0 found in ponderated array : " + item.ToString ());
				}
				total += item.GetWeight ();
			}
			int result = Random.Range (1, total + 1);
			foreach (T item in items) {
				result -= item.GetWeight ();
				if (result <= 0) {
					return item;
				}
			}
			//Ne devrait jamais arriver, mais je dois le mettre pour le compilateur.
			return default(T);
		}

		public T GetRandomItem<T> (T[] items)
		{
			int position = Random.Range (0, items.Length - 1);
			return items [position];
		}
	}
}
