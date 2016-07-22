using UnityEngine;
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
				if(offsetDistance - offsetDistanceDone <= 0){
					distance += e.Data;				
				}else{
					offsetDistanceDone += e.Data;
				}
			}));
		}

		public static string version = "0.1.0";

		private static float distance = 0f;

		private static float offsetDistance = 10f;

		private static float offsetDistanceDone = 0f;

        public static readonly float WORLD_LENGTH = 8000f;

		public static class Config
		{
			public static readonly int STAT_POINTS_PER_LEVEL = 5;
			public static readonly float BASE_REGEN_RATIO = 0.1f;
		}

		public static class Colors
		{
			public static Color PURPLE = new Color (255, 0, 255);
		}

		public static float GetDistance ()
		{
			return distance;
		}

		public void StartGame ()
		{
			SceneManager.LoadScene (gameScene);
			distance = worldManager.GetCheckpoint();
		}

		private void GameOver ()
		{
			EventDispatcher.Instance.Dispatch ("save");
			offsetDistanceDone = 0f;
            gameOver.Init (distance);
            gameOver.gameObject.SetActive (true);
		}

		public void Restart ()
		{
			distance = worldManager.GetCheckpoint();
            spawnEngine.Clear ();
			EventDispatcher.Instance.Clear ();
			SceneManager.LoadScene (gameScene);
		}
	}
}
