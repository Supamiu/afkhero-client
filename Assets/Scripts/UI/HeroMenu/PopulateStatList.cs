using UnityEngine;

using AFKHero.Stat;

namespace AFKHero.UI.HeroMenu
{
    public class PopulateStatList : MonoBehaviour
    {
        public GameObject hero;
        public StatElement prefab;

        // Use this for initialization
        void Start()
        {
            AbstractStat[] stats = hero.GetComponents<AbstractStat>();

            foreach (AbstractStat stat in stats)
            {
                if (stat.GetStatType() == StatType.PRIMARY)
                {
                    StatElement instance = Instantiate(prefab);
                    instance.transform.SetParent(gameObject.transform, false);
                    instance.SetStat(stat);
                }
            }
        }
    }
}
