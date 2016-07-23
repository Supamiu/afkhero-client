using AFKHero.Behaviour;
using AFKHero.Core.Event;
using AFKHero.Core.Save;
using AFKHero.EventData;
using UnityEngine;

namespace AFKHero.Stat
{
    public class Defense : AbstractStat
    {
        private Damageable damageable;

        void Start()
        {
            damageable = GetComponent<Damageable>();
            EventDispatcher.Instance.Register("attack.compute", new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> e) => {
                if (e.Data.target == damageable)
                {
                    e.Data.damageReduction = Value / 5000 + Value;
                }
            }, 1000));
        }

        public override void Add(int amount){}

        public override void DoLoad(SaveData data){}

        public override string GetName()
        {
            return "defense";
        }

        public override StatType GetStatType()
        {
            return StatType.SECONDARY;
        }

        public override SaveData Save(SaveData save)
        {
            return save;
        }
    }
}
