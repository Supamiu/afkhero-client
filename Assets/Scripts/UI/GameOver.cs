using UnityEngine;
using UnityEngine.UI;
using AFKHero.Common;

namespace AFKHero.UI
{
    public class GameOver : MonoBehaviour
	{

		public Text distance;

		public void Init (float pDistance)
		{
			distance.text = Formatter.ToDistanceString (pDistance);
		}
	}
}
