namespace AFKHero.EventData
{
    public class SpeedCompute
    {
        public float amount;

        public float bonus = 1f;

        public SpeedCompute(float amount)
        {
            this.amount = amount;
        }

        public float Value
        {
            get
            {
                return amount * bonus;
            }
        }
       
    }
}
