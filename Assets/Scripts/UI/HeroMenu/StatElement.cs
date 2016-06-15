using UnityEngine;
using System.Collections;

using AFKHero.Stat;
using UnityEngine.UI;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.UI.HeroMenu
{
	public class StatElement : MonoBehaviour {

		private IListener listener;
		private AbstractStat stat;
		public Text text;

		void Start() {				
			// Récupération de l'event ui.stat.updated pour mettre à jour le texte
			this.listener = new Listener<GenericGameEvent<AbstractStat>> ((ref GenericGameEvent<AbstractStat> gameEvent) => {
				if (gameEvent.Data == this.stat)
				{
					this.SetText (stat);
				}
			}, -100);
			EventDispatcher.Instance.Register ("ui.stat.updated", this.listener);
		}

		public void SetStat(AbstractStat stat) {
			this.stat = stat;
			this.SetText (stat);
		}

		private void SetText(AbstractStat stat) {
			this.text.text = stat.GetName () + " " + stat.amount;
		}

		public void increaseStat(int value) {
			EventDispatcher.Instance.Dispatch("ui.stat.increase", new GenericGameEvent<StatIncrease>(new StatIncrease(this.stat, value)));
		}
	}
}