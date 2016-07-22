using UnityEngine;

namespace AFKHero.UI
{
    public class PopupMenu : MonoBehaviour
	{
		private Menu[] menus;

		public GameObject layout;

		// Use this for initialization
		void Start ()
		{
            menus = layout.GetComponentsInChildren<Menu> ();
			foreach (Menu m in menus) {
				m.Hide ();
			}
            Close();
		}

		/// <summary>
		/// Show the specified menu.
		/// </summary>
		/// <param name="menuName">Menu name.</param>
		public void Show (string menuName)
		{
            layout.SetActive (true);
			foreach (Menu m in menus) {
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
			foreach (Menu m in menus) {
				if (m.id == menuName) {
					if (m.IsShown ()) {
						m.Hide ();
                        Close();
					} else {
                        layout.SetActive (true);
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
            layout.SetActive (false);
		}
	}
}
