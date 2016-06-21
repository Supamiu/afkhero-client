using UnityEngine;
using System.Collections;
using AFKHero.Common;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.UI.CombatText
{
	public class CombatTextController : MonoBehaviour {
		public CombatText prefab;
		public GameObject canvas;

		private IListener listener;

		void Start ()
		{
			this.listener = new Listener<GenericGameEvent<Damage>> ((ref GenericGameEvent<Damage> gameEvent) => {
				if(gameEvent.Data.hits){
					if(gameEvent.Data.critical){
						this.CreateCombatText(Formatter.Format(gameEvent.Data.damage)+"!!", gameEvent.Data.target.transform);
					}else{
						this.CreateCombatText(Formatter.Format(gameEvent.Data.damage), gameEvent.Data.target.transform);
					}
				}else{
					this.CreateCombatText("Miss !", gameEvent.Data.target.transform);
				}
			}, -100);

			EventDispatcher.Instance.Register ("attack.damage", this.listener);
		}

		private void CreateCombatText(string text, Transform location)
		{
			CombatText instance = Instantiate (prefab);

			instance.transform.SetParent (canvas.transform, false);
			instance.transform.position = location.position;
			instance.SetText (text);
		}
	}
}