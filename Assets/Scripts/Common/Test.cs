using UnityEngine;
using System.Collections;
using AFKHero.Core.Event;

using System;

namespace AFKHero.Common{
	
	public class Test : MonoBehaviour
	{
		/// <summary>
		/// Fonction de test bateau
		/// </summary>
		public void TestFormatter ()
		{
				System.Random rdm = new System.Random ();

				double value = 0D;
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 1D;
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 10D;
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 665D;
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D;
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 1);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 2);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 3);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 4);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 5);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 6);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 7);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 8);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 9);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 17);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 29);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 43);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 68);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 89);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 92);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));

				value = 6659D * Math.Pow(10, 100);
				Debug.Log ("Value : " + value + " ==> " + Formatter.Format(value));
			}
	}
}

