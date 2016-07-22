using System;
using AFKHero.Core.Save;

namespace AFKHero.Stat
{
    /// <summary>
    /// Affecte les damages d'attaque;
    /// </summary>
    public class Strength : AbstractStat
	{
		public override void Add (int amount)
		{
			this.amount += amount;
		}

		public override SaveData Save(SaveData data){
			data.strength = amount;
			return data;
		}

		public override void DoLoad (SaveData data){
            amount = data.strength;
		}

		public override string GetName ()
		{
			return "strength";
		}

        public override StatType GetStatType()
        {
            return StatType.PRIMARY;
        }
    }
}
