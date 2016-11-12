using UnityEngine;
using AFKHero.Behaviour;
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
	    private void Start ()
		{
            maxWidth = hpBarRect.sizeDelta.x;
            vitality = GetComponentInParent<Vitality> ();
            monster = GetComponentInParent<Damageable> ();
            monster.OnDamaged += () => {
				gameObject.SetActive(true);
                hpBarRect.sizeDelta = new Vector2(maxWidth * (float)(vitality.currentHp / vitality.Value), hpBarRect.sizeDelta.y);
			};
			gameObject.SetActive (false);
		}


	}
}
