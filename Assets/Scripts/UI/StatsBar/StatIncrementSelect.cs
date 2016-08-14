using UnityEngine;
using System.Collections;

namespace AFKHero.UI.StatsBar
{
	public class StatIncrementSelect : MonoBehaviour
	{
		public RectTransform container;
		public bool isOpen;
		private int value;

		// Use this for initialization
		void Start ()
		{
			value = 1;
		}

		// Update is called once per frame
		void Update ()
		{
			Vector2 scale = container.localScale;
			scale.x = Mathf.Lerp (scale.x, isOpen ? 1 : 0, Time.deltaTime * 12);
			container.localScale = scale;
		}

		public void ToggleContainer ()
		{
			isOpen = !isOpen;
		}

		public void setValue (int value)
		{
			this.value = value;
		}

		public int getValue ()
		{
			return this.value;
		}
	}
}