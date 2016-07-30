using System;
using AFKHero.Behaviour;
using AFKHero.Core.Event;
using AFKHero.Core.Save;
using AFKHero.EventData;
using UnityEngine;
using AFKHero.Tools;

namespace AFKHero.Stat
{
    [RequireComponent(typeof(Damageable))]
    public class Defense : AbstractStat, IOnDeath
    {
        private Damageable damageable;

        private IListener listener1;
        private IListener listener2;

        void Awake()
        {
            listener1 = new Listener<GenericGameEvent<GearStat>>((ref GenericGameEvent<GearStat> e) =>
            {
                if (e.Data.subject == gameObject)
                {
                    amount += e.Data.amount;
                }
            });

            listener2 = new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> e) =>
            {
                if (e.Data.target == damageable)
                {
                    e.Data.damageReductionPercent = RatioEngine.Instance.GetDamageReductionPercent(Value);
                }
            }, 1000);
            amount = 0;
            damageable = GetComponent<Damageable>();
            EventDispatcher.Instance.Register("attack.compute", listener2);
            EventDispatcher.Instance.Register("gearstat.defense", listener1);
        }

        public override void Add(int amount) { }

        public override void DoLoad(SaveData data) { }

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

        public void OnDeath()
        {
            EventDispatcher.Instance.Unregister("gearstat.defense", listener1);
            EventDispatcher.Instance.Unregister("attack.compute", listener2);
        }
    }
}
