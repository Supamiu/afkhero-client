using UnityEngine;

namespace AFKHero.Stat
{
    public class Defense : MonoBehaviour
    {
        public double amount = 1;

        public float ratio = 1;

        public double Value
        {
            get
            {
                return amount * ratio;
            }
        }
    }
}
