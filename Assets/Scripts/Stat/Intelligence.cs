using System;
using AFKHero.Core.Save;

namespace AFKHero.Stat
{
    /// <summary>
    /// Influence les damages de l'attaque par clic.
    /// </summary>
    public class Intelligence : AbstractStat {

		public override void Add (int amount)
		{
			this.amount += amount;
		}

		public override SaveData Save(SaveData data){
			data.intelligence = amount;
			return data;
		}

		public override void DoLoad (SaveData data){
            amount = data.intelligence;
		}

		public override string GetName() {
			return "intelligence";
		}

        public override StatType GetStatType()
        {
            return StatType.PRIMARY;
		}

		public override string GetAbbreviation() 
		{
			return "Int";
		}
    }
}
