using AFKHero.Core.Event;
using AFKHero.Core.Save;

namespace AFKHero.Stat
{
    public class Vitality : AbstractStat
	{
		public double currentHp;

		public override void Add (int points)
		{
			var currentRatio = (float)currentHp / (float)Value;
            amount += points;
            currentHp = currentRatio * Value;
			if (OnVitalityUpdated != null) {
                OnVitalityUpdated.Invoke();
			}
		}

		public double heal (double pAmount)
		{
			var healed = pAmount;
			if (currentHp + pAmount <= Value) {
                currentHp += pAmount;
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

	    private void Start ()
        {
            currentHp = Value;
            EventDispatcher.Instance.Register(Events.Stat.Health.FULL_HEAL, new Listener<GameEvent>((ref GameEvent e) =>
            {
                currentHp = Value;
                if (OnVitalityUpdated != null)
                {
                    OnVitalityUpdated.Invoke();
                }
            }));

            EventDispatcher.Instance.Register(Events.Gear.MODIFIED, new Listener<GameEvent>((ref GameEvent e) =>
            {
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

		public override string GetAbreviation() 
		{
			return "Vit";
		}
    }
}
