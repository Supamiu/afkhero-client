using UnityEngine;
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
            damageListener = new Listener<GenericGameEvent<Damage>> ((ref GenericGameEvent<Damage> gameEvent) => {
				if (gameEvent.Data.hits) {
					if (gameEvent.Data.critical) {
                        CreateCombatText(Formatter.Format (gameEvent.Data.damage) + "!!", gameEvent.Data.target.transform, CombatTextType.DAMAGE);
					} else {
                        CreateCombatText(Formatter.Format (gameEvent.Data.damage), gameEvent.Data.target.transform, CombatTextType.DAMAGE);
					}
				} else {
                    CreateCombatText("Miss !", gameEvent.Data.target.transform, CombatTextType.MISS);
				}
			}, -100);

            healListener = new Listener<GenericGameEvent<Heal>> ((ref GenericGameEvent<Heal> e) => {
                CreateCombatText(Formatter.Format (e.Data.amount), e.Data.target.transform, CombatTextType.HEAL);
			});

			EventDispatcher.Instance.Register ("attack.damage", damageListener);
			EventDispatcher.Instance.Register ("heal", healListener);
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