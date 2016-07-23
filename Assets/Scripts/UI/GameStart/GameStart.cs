using UnityEngine;
using AFKHero.Core;
using UnityEngine.UI;

namespace AFKHero.UI.GameStart
{
    public class GameStart : MonoBehaviour
	{
		public WorldManager manager;

		public Text worldText;

		void Start(){
            worldText.text = "Monde " + (manager.GetCurrentWorldIndex() + 1) + " : " + manager.GetCurrentWorld ().worldName;
		}
	}
}
