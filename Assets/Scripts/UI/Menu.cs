using UnityEngine;

namespace AFKHero.UI
{
    public class Menu : MonoBehaviour
	{
		public string id;

		public void Show(){
            gameObject.SetActive (true);
		}

		public void Hide(){
            gameObject.SetActive (false);
		}

		public bool IsShown(){
			return gameObject.activeSelf;
		}
	}
}
