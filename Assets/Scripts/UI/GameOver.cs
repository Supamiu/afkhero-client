using UnityEngine;
using UnityEngine.UI;
using AFKHero.Common;

namespace AFKHero.UI
{
    public class GameOver : MonoBehaviour
	{

		public Text distance;

		public void Init (float distance)
		{
			this.distance.text = Formatter.ToDistanceString (distance);
		}
	}
}
