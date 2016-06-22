using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using AFKHero.Core.Event;
using AFKHero.Behaviour;
using AFKHero.Core;
using AFKHero.UI;

namespace AFKHero
{
	
	public class AFKHero : MonoBehaviour
	{

		[Header ("Référence vers le héro pour l'écran de GameOver")]
		public Damageable hero;

		[Header ("La vue de GameOver")]
		public GameOver gameOver;

		[Header ("Le gestionnaire de mondes")]
		public WorldManager worldManager;

		[Header ("Le moteur de spawns")]
		public SpawnEngine spawnEngine;

		[Header ("scène principale de jeu")]
		public string gameScene = "Game";

		void Start ()
		{
			hero.onDeath += () => {
				Invoke ("GameOver", 0.5f);
			};
			Application.targetFrameRate = 60;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			EventDispatcher.Instance.Register ("movement.moved", new Listener<GenericGameEvent<float>> ((ref GenericGameEvent<float> e) => {
				distance += e.Data;
			}));
		}

		public static string version = "0.1.0";

		private static float distance = 0f;

		private static float offsetDistance = 20f;

		public static class Config
		{
			public static readonly int STAT_POINTS_PER_LEVEL = 5;
		}

		public static class Colors
		{
			public static Color PURPLE = new Color (255, 0, 255);
		}

		public static float GetDistance ()
		{
			return distance - offsetDistance > 0f ? distance - offsetDistance : 0f;
		}

		public void StartGame ()
		{
			SceneManager.LoadScene (gameScene);
			distance = this.worldManager.GetCurrentWorld ().start - 20f;
		}

		private void GameOver ()
		{
			EventDispatcher.Instance.Dispatch ("save");
			this.gameOver.Init (distance);
			this.gameOver.gameObject.SetActive (true);
		}

		public void Restart ()
		{
			distance = this.worldManager.GetCurrentWorld ().start;
			this.spawnEngine.Clear ();
			EventDispatcher.Instance.Clear ();
			SceneManager.LoadScene (gameScene);
		}
	}
}
