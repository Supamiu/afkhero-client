using UnityEngine;
using UnityEngine.UI;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Stat;


namespace AFKHero.Debugger
{
    public class StatsDebugger : MonoBehaviour
	{
		public Text level;

		public Text xp;

		public Text gold;

		public Text hp;

		private double hpActual = 5;

		private double hpMax = 5;

		private double goldAmount;

	    private void Start ()
		{
			EventDispatcher.Instance.Register (Events.Level.UP, new Listener<GenericGameEvent<LevelUp>> ((ref GenericGameEvent<LevelUp> e) => {
                level.text = "Level : " + e.Data.level;
                xp.text = "XP : " + e.Data.xpRemaining + " / " + e.Data.xpForNextLevel;
			}, -5000));

			EventDispatcher.Instance.Register (Events.UI.EXPERIENCE, new Listener<GenericGameEvent<XPGain>> ((ref GenericGameEvent<XPGain> e) => {
                xp.text = "XP : " + e.Data.xp + " / " + e.Data.xpForNextLevel;
			}, -1));

			EventDispatcher.Instance.Register (Events.GOLD_GAIN, new Listener<GenericGameEvent<double>> ((ref GenericGameEvent<double> e) => {
                goldAmount += e.Data;
                gold.text = "Gold : " + goldAmount;
			}, -1));

			EventDispatcher.Instance.Register (Events.Attack.DAMAGE, new Listener<GenericGameEvent<Damage>> ((ref GenericGameEvent<Damage> e) => {
			    if (e.Data.target.gameObject.name != "Hero") return;
			    hpActual -= e.Data.damage;
			    hp.text = "HP : " + hpActual + "/" + hpMax;
			}, -1));

			EventDispatcher.Instance.Register (Events.UI.STAT_UPDATED, new Listener<GenericGameEvent<AbstractStat>> ((ref GenericGameEvent<AbstractStat> e) => {
			    if (e.Data.GetName() != "vitality") return;
			    hpActual = ((Vitality)e.Data).currentHp;
			    hpMax = e.Data.Value;
			    hp.text = "HP : " + ((Vitality)e.Data).currentHp + "/" + e.Data.Value;
			}, -1));
		}
	}
}