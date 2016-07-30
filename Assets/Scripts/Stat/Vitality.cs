using AFKHero.Core.Event;
using AFKHero.Core.Save;

namespace AFKHero.Stat
{
    public class Vitality : AbstractStat
	{
		public double currentHp;

		public override void Add (int points)
		{
			float ratio = (float)currentHp / (float)Value;
            amount += points;
            currentHp = ratio * Value;
			if (OnVitalityUpdated != null) {
                OnVitalityUpdated.Invoke();
			}
		}

		public double heal (double amount)
		{
			double healed = amount;
			if (currentHp + amount <= Value) {
                currentHp += amount;
			} else {
				healed = Value - currentHp;
                currentHp = Value;
			}
			if (OnVitalityUpdated != null) {
                OnVitalityUpdated.Invoke();
			}
			return healed;
		}

		public delegate void UpdateEvent ();

		public event UpdateEvent OnVitalityUpdated;

		void Start ()
        {
            currentHp = Value;
            EventDispatcher.Instance.Register("health.fullHeal", new Listener<GameEvent>((ref GameEvent e) =>
            {
                currentHp = Value;
                if (OnVitalityUpdated != null)
                {
                    OnVitalityUpdated.Invoke();
                }
            }));
        }

		public void Init ()
		{
            Start();
		}

		public override string GetName ()
		{
			return "vitality";
		}

		public override SaveData Save (SaveData data)
		{
			data.vitality = amount;
			return data;
		}

		public override void DoLoad (SaveData data)
		{
            amount = data.vitality;
			if (OnVitalityUpdated != null) {
                OnVitalityUpdated.Invoke();
			}
		}

        public override StatType GetStatType()
        {
            return StatType.PRIMARY;
        }
    }
}
