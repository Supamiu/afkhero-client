using UnityEngine;
using AFKHero.EventData;
using AFKHero.Core.Event;
using AFKHero.Tools;

namespace AFKHero.Core
{
	public class CritEngine : MonoBehaviour
	{
	    private void Start ()
		{
			EventDispatcher.Instance.Register ("attack.compute", new Listener<GenericGameEvent<Attack>> ((ref GenericGameEvent<Attack> e) => {
				e.Data.critical = PercentageUtils.Instance.GetResult (e.Data.critChances);
			}, 2000));
		}
	}
}
