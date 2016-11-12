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
	    private void Start()
		{
			menus = GetComponentsInChildren<Menu>(true);
			Close();
		}

		/// <summary>
		/// Show the specified menu.
		/// </summary>
		/// <param name="menuName">Menu name.</param>
		public void Show(string menuName)
		{
			if (null != activeMenu && menuName == activeMenu.id) {
				Close ();
			} else {
				GetComponent<Image> ().enabled = true;
				foreach (var m in menus) {
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
			if (targetMenu != null) {
				SetActiveMenu (targetMenu);
			}
			foreach (var m in menus)
			{
			    if (m.id != menuName) continue;
			    targetMenu = m;
			    direction = "previous";
			    m.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-1080, 0);
			    m.Show();
			}
		}

		public void SlideToNext(string menuName)
		{
			if (targetMenu != null) {
				SetActiveMenu (targetMenu);
			}
			foreach (var m in menus)
			{
			    if (m.id != menuName) continue;
			    targetMenu = m;
			    direction = "next";
			    m.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (1080, 0);
			    m.Show();
			}
		}

	    private void Update()
		{
		    if (null == targetMenu || targetMenu.id == activeMenu.id) return;
		    var activeX = Mathf.Lerp (activeMenu.GetComponent<RectTransform> ().anchoredPosition.x, direction == "next" ? -1080 : 1080, Time.deltaTime * 12);
		    var targetX = Mathf.Lerp (targetMenu.GetComponent<RectTransform> ().anchoredPosition.x, 0.0f, Time.deltaTime * 12);

		    if (targetX <= 0.1f && targetX >= -0.1f) {
		        SetActiveMenu (targetMenu);
		    } else {
		        activeMenu.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (activeX, 0);
		        targetMenu.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (targetX, 0);
		    }
		}

		private void SetActiveMenu(Menu m)
		{
			if (activeMenu != null) {
				activeMenu.Hide ();
			}

			m.Show ();
			m.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 0);
			targetMenu = null;
			activeMenu = m;
		}

		/// <summary>
		/// Close the menu.
		/// </summary>
		public void Close()
		{
			activeMenu = null;
			targetMenu = null;
			GetComponent<Image> ().enabled = false;
			foreach (var m in menus)
			{
				m.Hide();
			}
		}
	}
}
