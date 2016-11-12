using UnityEngine;
using UnityEngine.UI;

namespace AFKHero.UI.CombatText
{
    public class CombatText : MonoBehaviour {
		private Text damageText;

        private float opacity;

        private void OnEnable() {
			damageText = GetComponent<Text> ();
			opacity = damageText.color.a;
		}

        private void Start()  {
            var vector = new Vector2
            {
                x = Random.Range(-4, 4),
                y = Random.Range(10, 10)
            };

            GetComponent<Rigidbody2D> ().gravityScale = 3;
			GetComponent<Rigidbody2D>().AddForce (vector, ForceMode2D.Impulse);
		}

        private void OnCollisionEnter2D(Collision2D collision)
		{
			opacity = 0;
		}

        private void Update() 
		{
			if (damageText.color.a <= 0.1f) {
				Destroy (gameObject);
			} else {
				var color = damageText.color;
				color.a -= (color.a - opacity) * Time.deltaTime * 2;
				damageText.color = color;
			}
		}

		public void SetText(string text)
		{
			damageText.text = text;
		}

		public void SetColor(Color color){
			damageText.color = color;
		}
	}
}
