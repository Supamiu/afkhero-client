using UnityEngine;
using System.Collections;
using AFKHero.Common;

namespace AFKHero.Stat{
	public abstract class Stat : MonoBehaviour {

		/// <summary>
		/// Montant actuel contenu dans la stat
		/// </summary>
		public double amount = 1;

		/// <summary>
		/// Ratio qui régis la relation entre le montant et la valeur servant aux calculs.
		/// </summary>
		public float ratio = 1;

		public double Value{ 
			get { 
				return this.amount * this.ratio;
			} 
		}
	}
}
