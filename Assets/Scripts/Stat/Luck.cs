﻿using AFKHero.Core.Save;

namespace AFKHero.Stat
{
    /// <summary>
    /// Affacte les taux de loot.
    /// </summary>
    public class Luck : AbstractStat
	{

		public override void Add (int pAmount)
		{
			amount += pAmount;
		}

		public override SaveData Save(SaveData data){
			data.luck = amount;
			return data;
		}

		public override void DoLoad (SaveData data){
            amount = data.luck;
		}

		public override string GetName ()
		{
			return "luck";
		}

        public override StatType GetStatType()
        {
            return StatType.PRIMARY;
		}

		public override string GetAbreviation() 
		{
			return "Lck";
		}
    }
}
