using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using AFKHero.Core.Event;

namespace AFKHero{
	
	public class AFKHero : MonoBehaviour{

		void Start(){
			Application.targetFrameRate = 30;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			EventDispatcher.Instance.Register ("movement.moved", new Listener<GenericGameEvent<float>> ((ref GenericGameEvent<float> e) => {
				distance += e.Data;
			}));
		}

		public static string version = "0.1.0";

		public static float distance = 0f;

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
