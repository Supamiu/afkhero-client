using UnityEngine;

using AFKHero.Stat;

namespace AFKHero.UI.StatsBar
{
	public class PopulateStatButtonList : MonoBehaviour
	{
		public GameObject hero;
		public StatButton prefab;
		public StatIncrementSelect incrementSelect;

		// Use this for initialization
	    private void Start()
		{
			var stats = hero.GetComponents<AbstractStat>();

			foreach (var stat in stats)
			{
			    if (stat.GetStatType() != StatType.PRIMARY) continue;
			    var instance = Instantiate(prefab);
			    instance.SetStat (stat);
			    instance.SetIncrementSelect (incrementSelect);
			    instance.transform.SetParent(gameObject.transform, false);
			}
		}
	}
}
