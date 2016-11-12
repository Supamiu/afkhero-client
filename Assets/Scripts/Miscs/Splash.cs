using UnityEngine;
using UnityEngine.SceneManagement;
namespace AFKHero.Miscs
{
    /// <summary>
    /// Un splashscreen simple qui défini une durée avant de passer à la scène Menu.
    /// </summary>
    public class Splash : MonoBehaviour {

		/// <summary>
		/// La durée du splash.
		/// </summary>
		[Header("Splash duration")]
		public float duration = 2;

		// Use this for initialization
        private void Start () {
			Invoke ("OpenMenu", duration);
		}

        private static void OpenMenu(){
			SceneManager.LoadScene ("Menu");
		}
	}
}
