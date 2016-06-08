using UnityEngine;
using System.Collections;
using AFKHero.Core.Tools;

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
			return (percentage * 100) > Random.Range (1, 100);
		}
	}
}
