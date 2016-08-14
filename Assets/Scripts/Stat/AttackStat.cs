using AFKHero.Behaviour;
using AFKHero.Core.Event;
using AFKHero.Core.Save;
using AFKHero.EventData;
using UnityEngine;

namespace AFKHero.Stat
{
    [RequireComponent(typeof(Agressive))]
    [RequireComponent(typeof(Strength))]
    public class AttackStat : AbstractStat
    {
        private Agressive agressive;

        private Strength strength;

        void Awake()
        {
            amount = 0;
            agressive = GetComponent<Agressive>();
            strength = GetComponent<Strength>();

            EventDispatcher.Instance.Register("attack.compute", new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> gameEvent) =>
            {
                if (gameEvent.Data.attacker == agressive)
                {
                    gameEvent.Data.baseDamage = (1 + strength.Value / 10d) * Value;
                }
            }, 4000));

            EventDispatcher.Instance.Register("gearstat.attack", new Listener<GenericGameEvent<GearStat>>((ref GenericGameEvent<GearStat> e) =>
            {
                if (e.Data.subject == gameObject)
                {
                    amount += e.Data.amount;
                }
            }));
        }

        public override void Add(int amount)
        {
            this.amount += amount;
        }

        public override void DoLoad(SaveData data){}

        public override string GetName()
        {
            return "attack";
        }

        public override StatType GetStatType()
        {
            return StatType.SECONDARY;
        }

        public override SaveData Save(SaveData save)
        {
            return save;
		}

		public override string GetAbbreviation() 
		{
			return "Atk";
		}
    }
}
