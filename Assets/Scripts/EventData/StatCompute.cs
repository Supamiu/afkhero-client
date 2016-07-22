using AFKHero.Stat;

namespace AFKHero.EventData
{
    public class StatCompute
    {
        public double amount;

        public float ratio;

        public AbstractStat stat { get; private set; }

        public StatCompute(AbstractStat stat, double amount, float ratio)
        {
            this.amount = amount;
            this.ratio = ratio;
            this.stat = stat;
        }
    }
}
