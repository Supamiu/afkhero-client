using UnityEngine;
using System.Collections;
using AFKHero.Common;
using System;
using AFKHero.Core.Event;

namespace AFKHero.Stat{
	public abstract class AbstractStat : MonoBehaviour {

		/// <summary>
		/// Montant actuel contenu dans la stat
		/// </summary>
		public double amount = 1;

		/// <summary>
		/// Ratio qui régis la relation entre le montant et la valeur servant aux calculs.
		/// </summary>
		public float ratio = 1;

		public abstract void Add(int amount);

		public abstract string GetName();

		public double Value{ 
			get { 
				return ((GenericGameEvent<double>)EventDispatcher.Instance.Dispatch("stat.compute."+this.GetName(), new GenericGameEvent<double>(Math.Round(this.amount * this.ratio)))).Data;
			} 
		}
	}
}
