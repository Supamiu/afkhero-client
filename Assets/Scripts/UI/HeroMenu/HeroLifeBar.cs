using UnityEngine;
using UnityEngine.UI;
using System.Collections;
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
			this.barRect = this.bar.GetComponent<RectTransform> ();
			this.maxWidth = this.barRect.sizeDelta.x;
			this.hp = hero.GetComponent <Vitality> ();
			this.hero.OnDamaged += () => {
				this.UpdateBar ();
			};
			this.hp.OnVitalityUpdated += () => {
				this.UpdateBar ();
			};
			this.UpdateBar ();
		}

		void UpdateBar ()
		{
			this.percent = (float)(this.hp.currentHp / this.hp.Value);
			if (percent <= 0.33f) {
				this.bar.color = Color.red;
			} else if (percent <= 0.66f) {
				this.bar.color = Color.yellow;
			} else {
				this.bar.color = Color.green;
			}
			double hpValue = this.hp.currentHp > 0 ? this.hp.currentHp : 0;
			this.innerText.text = Formatter.Format (Math.Round (hpValue)) + "/" + Formatter.Format (Math.Round (this.hp.Value));
		}

		void Update ()
		{
			this.barRect.sizeDelta = Vector2.Lerp (this.barRect.sizeDelta, new Vector2 (this.maxWidth * this.percent, this.barRect.sizeDelta.y), Time.deltaTime);
		}
	}
}
