using UnityEngine;
using UnityEngine.UI;

namespace AFKHero.UI
{
	public class SlideMenu : MonoBehaviour
	{
		private Menu[] menus;
		private Menu activeMenu;
		private Menu targetMenu;
		private string direction;

		// Use this for initialization
		void Start()
		{
			this.menus = GetComponentsInChildren<Menu>(true);
			Close();
		}

		/// <summary>
		/// Show the specified menu.
		/// </summary>
		/// <param name="menuName">Menu name.</param>
		public void Show(string menuName)
		{
			if (null != this.activeMenu && menuName == activeMenu.id) {
				Close ();
			} else {
				GetComponent<Image> ().enabled = true;
				foreach (Menu m in menus) {
					if (m.id == menuName) {
						SetActiveMenu (m);
					} else {
						m.Hide ();
					}
				}
			}
		}

		public void SlideToPrevious(string menuName)
		{
			if (this.targetMenu != null) {
				SetActiveMenu (this.targetMenu);
			}
			foreach (Menu m in menus)
			{
				if (m.id == menuName)
				{
					this.targetMenu = m;
					this.direction = "previous";
					m.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-1080, 0);
					m.Show();
				}
			}
		}

		public void SlideToNext(string menuName)
		{
			if (this.targetMenu != null) {
				SetActiveMenu (this.targetMenu);
			}
			foreach (Menu m in menus)
			{
				if (m.id == menuName)
				{
					this.targetMenu = m;
					this.direction = "next";
					m.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (1080, 0);
					m.Show();
				}
			}
		}

		void Update()
		{
			if (null != this.targetMenu && this.targetMenu.id != this.activeMenu.id)
			{
				float activeX = Mathf.Lerp (this.activeMenu.GetComponent<RectTransform> ().anchoredPosition.x, this.direction == "next" ? -1080 : 1080, Time.deltaTime * 12);
				float targetX = Mathf.Lerp (this.targetMenu.GetComponent<RectTransform> ().anchoredPosition.x, 0.0f, Time.deltaTime * 12);

				if (targetX <= 0.1f && targetX >= -0.1f) {
					SetActiveMenu (this.targetMenu);
				} else {
					this.activeMenu.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (activeX, 0);
					this.targetMenu.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (targetX, 0);
				}
			}
		}

		private void SetActiveMenu(Menu m)
		{
			if (this.activeMenu != null) {
				this.activeMenu.Hide ();
			}

			m.Show ();
			m.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
			this.targetMenu = null;
			this.activeMenu = m;
		}

		/// <summary>
		/// Close the menu.
		/// </summary>
		public void Close()
		{
			this.activeMenu = null;
			this.targetMenu = null;
			GetComponent<Image> ().enabled = false;
			foreach (Menu m in menus)
			{
				m.Hide();
			}
		}
	}
}
