using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace AFKHero{
	
	public class AFKHero : MonoBehaviour{

		void Start(){
			Application.targetFrameRate = 30;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

		public static string version = "0.0.1";
		public static class Config{
			
		}

		public static class Colors
		{
			public static Color PURPLE = new Color (255, 0, 255);
		}

		public void StartGame(){
			SceneManager.LoadScene ("Game");
		}
	}
}
