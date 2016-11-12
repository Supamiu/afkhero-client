using UnityEngine;
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

	    private void Start()
		{
			text.text = stat.GetAbreviation ();	
		}

		public void SetStat(AbstractStat pStat)
		{
			stat = pStat;
		}

		public void SetIncrementSelect(StatIncrementSelect pIncrementSelect)
		{
			incrementSelect = pIncrementSelect;
		}
	
		// Update is called once per frame
	    private void Update ()
		{
			value.text = Formatter.Format(stat.amount);		
		}

		public void IncrementStat ()
		{
			EventDispatcher.Instance.Dispatch ("ui.stat.increase", new GenericGameEvent<StatIncrease> (new StatIncrease (stat, incrementSelect.getValue ())));
		}
	}
}