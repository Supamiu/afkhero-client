using UnityEngine;
using System.Collections;
using AFKHero.Common;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.UI.CombatText
{
	public class CombatTextController : MonoBehaviour
	{
		public CombatText prefab;

		public GameObject parent;

		private IListener damageListener;

		private IListener healListener;

		void Start ()
		{
			this.damageListener = new Listener<GenericGameEvent<Damage>> ((ref GenericGameEvent<Damage> gameEvent) => {
				if (gameEvent.Data.hits) {
					if (gameEvent.Data.critical) {
						this.CreateCombatText (Formatter.Format (gameEvent.Data.damage) + "!!", gameEvent.Data.target.transform, CombatTextType.DAMAGE);
					} else {
						this.CreateCombatText (Formatter.Format (gameEvent.Data.damage), gameEvent.Data.target.transform, CombatTextType.DAMAGE);
					}
				} else {
					this.CreateCombatText ("Miss !", gameEvent.Data.target.transform, CombatTextType.MISS);
				}
			}, -100);

			this.healListener = new Listener<GenericGameEvent<Heal>> ((ref GenericGameEvent<Heal> e) => {
				this.CreateCombatText (Formatter.Format (e.Data.amount), e.Data.target.transform, CombatTextType.HEAL);
			});

			EventDispatcher.Instance.Register ("attack.damage", this.damageListener);
			EventDispatcher.Instance.Register ("heal", this.healListener);
		}

		private void CreateCombatText (string text, Transform location, CombatTextType type)
		{
			CombatText instance = Instantiate (prefab);

			instance.transform.SetParent (parent.transform, false);
			instance.transform.position = location.position;
			instance.SetColor (type.GetColor ());
			instance.SetText (text);
		}
	}
}