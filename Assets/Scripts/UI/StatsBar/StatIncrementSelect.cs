using UnityEngine;

namespace AFKHero.UI.StatsBar
{
	public class StatIncrementSelect : MonoBehaviour
	{
		public RectTransform container;
		public bool isOpen;
		private int value;

		// Use this for initialization
	    private void Start ()
		{
			value = 1;
		}

		// Update is called once per frame
	    private void Update ()
		{
			Vector2 scale = container.localScale;
			scale.x = Mathf.Lerp (scale.x, isOpen ? 1 : 0, Time.deltaTime * 12);
			container.localScale = scale;
		}

		public void ToggleContainer ()
		{
			isOpen = !isOpen;
		}

		public void setValue (int pValue)
		{
			value = pValue;
		}

		public int getValue ()
		{
			return value;
		}
	}
}