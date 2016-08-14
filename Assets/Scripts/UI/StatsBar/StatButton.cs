using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AFKHero.Stat;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Common;

namespace AFKHero.UI.StatsBar
{
	public class StatButton : MonoBehaviour
	{
		private AbstractStat stat;
		private StatIncrementSelect incrementSelect;
		public Text text;
		public Text value;

		void Start()
		{
			text.text = stat.GetAbbreviation ();	
		}

		public void SetStat(AbstractStat stat)
		{
			this.stat = stat;
		}

		public void SetIncrementSelect(StatIncrementSelect incrementSelect)
		{
			this.incrementSelect = incrementSelect;
		}

		void UpdateValue()
		{
			value.text = Formatter.Format (stat.amount);
		}
	
		// Update is called once per frame
		void Update ()
		{
			value.text = Formatter.Format(stat.amount);		
		}

		public void IncrementStat ()
		{
			EventDispatcher.Instance.Dispatch ("ui.stat.increase", new GenericGameEvent<StatIncrease> (new StatIncrease (stat, incrementSelect.getValue ())));
		}
	}
}