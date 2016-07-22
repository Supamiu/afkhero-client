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

		private RectTransform barRect;

		private Vitality hp;

		private float maxWidth;

		private float percent = 1f;

		void Start ()
		{
            barRect = bar.GetComponent<RectTransform> ();
            maxWidth = barRect.sizeDelta.x;
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
                bar.color = Color.green;
			}
			double hpValue = hp.currentHp > 0 ? hp.currentHp : 0;
            innerText.text = Formatter.Format (Math.Round (hpValue)) + "/" + Formatter.Format (Math.Round (hp.Value));
		}

		void Update ()
		{
            barRect.sizeDelta = Vector2.Lerp (barRect.sizeDelta, new Vector2 (maxWidth * percent, barRect.sizeDelta.y), Time.deltaTime);
		}
	}
}
