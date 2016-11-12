using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Behaviour;
using AFKHero.Tools;
using AFKHero.Core.Save;

namespace AFKHero.Stat
{
    /// <summary>
    /// Affecte les chances d'esquiver.
    /// </summary>
    public class Dodge : AbstractStat
	{
		private Damageable damageable;

	    private void Start ()
		{
            damageable = GetComponent<Damageable> ();
			EventDispatcher.Instance.Register (Events.Attack.COMPUTE, new Listener<GenericGameEvent<Attack>> ((ref GenericGameEvent<Attack> e) => {
			    if (e.Data.target != damageable) return;
			    var precision = e.Data.attacker.GetComponent<Agility> ().Value;
			    var hits = GetHits(precision);
			    e.Data.hits = hits;
			    if (!(precision > Value)) return;
			    var bonus = RatioEngine.Instance.GetCritBonus (precision, Value);
			    e.Data.critChances += bonus;
			}, 3000));
		}

		public override void Add (int pAmount)
		{
			amount += pAmount;
		}

		private bool GetHits (double precision)
		{
			return PercentageUtils.Instance.GetResult ((float)precision / (float)Value);
		}

		public override string GetName ()
		{
			return "dodge";
		}

		public override SaveData Save(SaveData data){
			return data;
		}

		public override void DoLoad (SaveData data){
		}

        public override StatType GetStatType()
        {
            return StatType.PRIMARY;
		}

		public override string GetAbreviation() 
		{
			return "Esq";
		}
    }
}
