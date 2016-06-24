using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AFKHero.Behaviour;
using AFKHero.Core.Event;
using AFKHero.EventData;
using AFKHero.Stat;

namespace AFKHero.UI
{
	public class MonsterHPBar : MonoBehaviour
	{
		public RectTransform hpBarRect;

		private Vector2 bar;

		private float maxWidth;

		private Damageable monster;

		private Vitality vitality;
		// Use this for initialization
		void Start ()
		{
			this.maxWidth = this.hpBarRect.sizeDelta.x;
			this.vitality = GetComponentInParent<Vitality> ();
			this.monster = GetComponentInParent<Damageable> ();
			this.monster.OnDamaged += () => {
				gameObject.SetActive(true);
				this.hpBarRect.sizeDelta = new Vector2(this.maxWidth * (float)(this.vitality.currentHp / this.vitality.Value), this.hpBarRect.sizeDelta.y);
			};
			gameObject.SetActive (false);
		}


	}
}
