using UnityEngine;
using System.Collections;

namespace AFKHero.UI
{
	public class Menu : MonoBehaviour
	{
		public string id;

		public void Show(){
			this.gameObject.SetActive (true);
		}

		public void Hide(){
			this.gameObject.SetActive (false);
		}

		public bool IsShown(){
			return this.gameObject.activeSelf;
		}
	}
}
