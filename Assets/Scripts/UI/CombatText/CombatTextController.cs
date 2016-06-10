using UnityEngine;
using System.Collections;
using AFKHero.Common;
using AFKHero.Core.Event;
using AFKHero.EventData;

public class CombatTextController : MonoBehaviour {

	public CombatText prefab;
	public GameObject canvas;

	private IListener listener;

	void Start ()
	{
		this.listener = new Listener<GenericGameEvent<Attack>> ((ref GenericGameEvent<Attack> gameEvent) => {
			this.CreateCombatText(Formatter.Format(gameEvent.Data.getDamage().damage), gameEvent.Data.target.transform);
		}, -100);

		EventDispatcher.Instance.Register ("attack", this.listener);
	}

	private void CreateCombatText(string text, Transform location)
	{
		CombatText instance = Instantiate (prefab);

		instance.transform.SetParent (canvas.transform, false);
		instance.transform.position = location.position;
		instance.SetText (text);
	}
}