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
		void Start()
		{
			AbstractStat[] stats = hero.GetComponents<AbstractStat>();

			foreach (AbstractStat stat in stats)
			{
				if (stat.GetStatType() == StatType.PRIMARY)
				{
					StatButton instance = Instantiate(prefab);
					instance.SetStat (stat);
					instance.SetIncrementSelect (incrementSelect);
					instance.transform.SetParent(gameObject.transform, false);
				}
			}
		}
	}
}
