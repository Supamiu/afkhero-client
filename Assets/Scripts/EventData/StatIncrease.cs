using UnityEngine;
using AFKHero.Stat;

namespace AFKHero.EventData
{
	public class StatIncrease
	{
		public AbstractStat stat;
		public int value;

		public StatIncrease (AbstractStat stat, int value)
		{
			this.stat = stat;
			this.value = value;
		}
	}
}