using UnityEngine;
using System.Collections;
using AFKHero.EventData;
using AFKHero.Core.Event;
using AFKHero.Tools;

namespace AFKHero.Core
{
	public class CritEngine : MonoBehaviour
	{

		// Use this for initialization
		void Start ()
		{
			EventDispatcher.Instance.Register ("attack.compute", new Listener<GenericGameEvent<Attack>> ((ref GenericGameEvent<Attack> e) => {
				e.Data.critical = PercentageUtils.Instance.GetResult (e.Data.critChances);
			}, 2000));
		}
	}
}
