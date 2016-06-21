using UnityEngine;
using System.Collections;
using AFKHero.Core;
using UnityEngine.UI;

namespace AFKHero.UI.GameStart
{
	public class GameStart : MonoBehaviour
	{
		public WorldManager manager;

		public Text worldText;

		void Start(){
			this.worldText.text = "Monde " + (manager.currentWorld + 1) + " : " + manager.GetCurrentWorld ().worldName;
		}
	}
}
