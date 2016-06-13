using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AFKHero.Core.Event;
using AFKHero.EventData;


namespace AFKHero.Debugger
{
	public class StatsDebugger : MonoBehaviour
	{
		public Text level;

		public Text xp;

		public Text gold;

		private double goldAmount = 0;

		void Start ()
		{
			EventDispatcher.Instance.Register ("level.up", new Listener<GenericGameEvent<LevelUp>> ((ref GenericGameEvent<LevelUp> e) => {
				this.level.text = "Level : " + e.Data.level;
				this.xp.text = "XP : " + e.Data.xpRemaining + " / " + e.Data.xpForNextLevel;
			}, -5000));

			EventDispatcher.Instance.Register ("experience.ui", new Listener<GenericGameEvent<XPGain>> ((ref GenericGameEvent<XPGain> e) => {
				this.xp.text = "XP : " + e.Data.xp + " / " + e.Data.xpForNextLevel;
			}, -1));

			EventDispatcher.Instance.Register ("gold", new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
				this.goldAmount += e.Data;
				this.gold.text = "Gold : " + this.goldAmount;
			}, -1));
		}
	}
}