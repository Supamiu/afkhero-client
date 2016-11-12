using AFKHero.Core.Save;

namespace AFKHero.Stat
{
    /// <summary>
    /// Influence la précision et le taux de critiques.
    /// </summary>
    public class Agility : AbstractStat
	{
		public override void Add (int pAmount)
		{
			amount += pAmount;
		}

		public override SaveData Save(SaveData data){
			data.agility = amount;
			return data;
		}

		public override void DoLoad (SaveData data){
            amount = data.agility;
		}

		public override string GetName ()
		{
			return "agility";
		}

        public override StatType GetStatType()
        {
            return StatType.PRIMARY;
		}

		public override string GetAbreviation()
		{
			return "Agi";
		}
    }
}
