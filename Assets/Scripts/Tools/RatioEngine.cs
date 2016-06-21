using UnityEngine;
using System.Collections;
using AFKHero.Core.Tools;

namespace AFKHero.Tools
{
	public class RatioEngine : Singleton<RatioEngine>
	{

		public double GetEnemyDamage (float damageRatio, float distance)
		{
			return (int)(damageRatio * (Mathf.Pow (distance - 10 / 2, 1.2f)) / 10);
		}

		public double GetEnemyHealth (float healthRatio, float distance)
		{
			return (int)(healthRatio * (Mathf.Pow (distance - 10, 1.5f) / 20) - 2);
		}

		public double GetEnemyDodge (float dodgeRatio, float distance)
		{
			if (distance < 50f) {
				distance = 0;
			}
			return dodgeRatio * distance / 10f;
		}

		public float GetCritBonus (double precision, double dodge)
		{
			return ((float)precision - (float)dodge) / 100f;
		}
	}
}
