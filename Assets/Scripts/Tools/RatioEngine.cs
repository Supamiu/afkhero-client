using UnityEngine;
using System.Collections;
using AFKHero.Core.Tools;

public class RatioEngine : Singleton<RatioEngine> {

	public double GetEnemyDamage(float damageRatio, float distance){
		return (int)(damageRatio * (Mathf.Pow(distance/2,1.2f)) / 10);
	}

	public double GetEnemyHealth(float healthRatio, float distance){
		return (int) (healthRatio * (Mathf.Pow (distance,1.5f)/20)-2) ;
	}
}
