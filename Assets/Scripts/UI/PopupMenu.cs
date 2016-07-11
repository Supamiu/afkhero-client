using UnityEngine;
using System.Collections;

namespace AFKHero.UI
{
	public class PopupMenu : MonoBehaviour
	{
		private Menu[] menus;

		public GameObject layout;

		// Use this for initialization
		void Start ()
		{
			this.menus = layout.GetComponentsInChildren<Menu> ();
			foreach (Menu m in this.menus) {
				m.Hide ();
			}
			this.Close ();
		}

		/// <summary>
		/// Show the specified menu.
		/// </summary>
		/// <param name="menuName">Menu name.</param>
		public void Show (string menuName)
		{
			this.layout.SetActive (true);
			foreach (Menu m in this.menus) {
				if (m.id == menuName) {
					m.Show ();
				} else {
					m.Hide ();
				}
			}
		}

		/// <summary>
		/// Toggle the specified menu.
		/// </summary>
		/// <param name="menuName">Menu name.</param>
		public void Toggle (string menuName)
		{
			foreach (Menu m in this.menus) {
				if (m.id == menuName) {
					if (m.IsShown ()) {
						m.Hide ();
						this.Close ();
					} else {
						this.layout.SetActive (true);
						m.Show ();
					}
				} else {
					m.Hide ();
				}
			}
		}

		/// <summary>
		/// Close the menu.
		/// </summary>
		public void Close(){
			this.layout.SetActive (false);
		}
	}
}
