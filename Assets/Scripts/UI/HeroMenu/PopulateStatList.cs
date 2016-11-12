using UnityEngine;

using AFKHero.Stat;

namespace AFKHero.UI.HeroMenu
{
    public class PopulateStatList : MonoBehaviour
    {
        public GameObject hero;
        public StatElement prefab;

        // Use this for initialization
        private void Start()
        {
            var stats = hero.GetComponents<AbstractStat>();

            foreach (var stat in stats)
            {
                if (stat.GetStatType() != StatType.PRIMARY) continue;
                var instance = Instantiate(prefab);
                instance.transform.SetParent(gameObject.transform, false);
                instance.SetStat(stat);
            }
        }
    }
}
