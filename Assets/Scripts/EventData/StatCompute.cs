using AFKHero.Stat;
using UnityEngine;

namespace AFKHero.EventData
{
    public class StatCompute
    {
        public double amount;

        public float ratio;

        public AbstractStat stat { get; private set; }

        public GameObject statOwner { get; private set; }

        public StatCompute(GameObject statOwner, AbstractStat stat, double amount, float ratio)
        {
            this.amount = amount;
            this.ratio = ratio;
            this.stat = stat;
            this.statOwner = statOwner;
        }
    }
}
