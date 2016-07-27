using UnityEngine;
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

        /// <summary>
        /// Récupère un item dans une liste de ponderables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Récupère un item aléatoire dans une liste.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
		public T GetRandomItem<T> (IList<T> items)
		{
			int position = Random.Range (0, items.Count - 1);
			return items [position];
		}

        /// <summary>
        /// Récupère un item aléatoire dans un tableau.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
		public T GetRandomItem<T>(T[] items)
        {
            int position = Random.Range(0, items.Length - 1);
            return items[position];
        }

        /// <summary>
        /// Récupère un float aléatoire entre deux valeurs données.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public float GetFloatInRange(float min, float max)
        {
            return Mathf.Ceil(Random.Range(min, max) * 100f) / 100f;
        }
	}
}
