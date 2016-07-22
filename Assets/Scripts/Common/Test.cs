using UnityEngine;
using AFKHero.Core.Event;
using AFKHero.EventData;

namespace AFKHero.Common
{

    public class Test : MonoBehaviour
	{
		/// <summary>
		/// Fonction de test bateau
		/// </summary>
		void Start ()
		{
			EventDispatcher.Instance.Register ("attack.compute", new Listener<GenericGameEvent<Attack>>((ref GenericGameEvent<Attack> e) => {
				bool crit = Random.Range(1,100)>50;
				e.Data.critical = crit;
				e.Data.criticalRatio = 1.5f;
			}, 1000));
		}
	}
}

