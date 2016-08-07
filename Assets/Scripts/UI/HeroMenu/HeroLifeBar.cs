using UnityEngine;
using UnityEngine.UI;
using AFKHero.Behaviour;
using AFKHero.Stat;
using AFKHero.Common;
using System;

namespace AFKHero.UI.HeroMenu
{
    public class HeroLifeBar : MonoBehaviour
	{
		public Image bar;

		public Text innerText;

		public Damageable hero;

		//private RectTransform barRect;
		private Image barImage;

		private Vitality hp;

		private float maxWidth;

		private float percent = 1f;

		void Start ()
		{
			barImage = bar.GetComponent<Image> ();

            //barRect = bar.GetComponent<RectTransform> ();
            //maxWidth = barRect.sizeDelta.x;

            hp = hero.GetComponent <Vitality> ();
            hero.OnDamaged += () => {
                UpdateBar();
			};
            hp.OnVitalityUpdated += () => {
                UpdateBar();
			};
            UpdateBar();
		}

		void UpdateBar ()
		{
            percent = (float)(hp.currentHp / hp.Value);
			if (percent <= 0.33f) {
                bar.color = Color.red;
			} else if (percent <= 0.66f) {
                bar.color = Color.yellow;
			} else {
				Color color = new Color ();
				ColorUtility.TryParseHtmlString("#51C304FF", out color);
				bar.color = color;
			}
			double hpValue = hp.currentHp > 0 ? hp.currentHp : 0;
            innerText.text = Formatter.Format (Math.Round (hpValue)) + "/" + Formatter.Format (Math.Round (hp.Value));
		}

		void Update ()
		{
			barImage.fillAmount -= (barImage.fillAmount - percent) * Time.deltaTime;
            //barRect.sizeDelta = Vector2.Lerp (barRect.sizeDelta, new Vector2 (maxWidth * percent, barRect.sizeDelta.y), Time.deltaTime);
		}
	}
}
