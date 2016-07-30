using UnityEngine;

namespace AFKHero.EventData
{
    public class GearStat
    {
        public int amount;

        public GameObject subject;
        
        public GearStat(int amount, GameObject subject)
        {
            this.amount = amount;
            this.subject = subject;
        }        
    }
}
