using UnityEngine;
using UnityEngine.UI;

namespace AFKHero.UI.CombatText
{
    public class CombatText : MonoBehaviour {
		private Text damageText;

		float opacity;

		void OnEnable() {
			damageText = GetComponent<Text> ();
			opacity = damageText.color.a;
		}

		void Start()  {
			Vector2 vector = new Vector2 ();
			vector.x = Random.Range (-4, 4);
			vector.y = Random.Range (10, 10);

			GetComponent<Rigidbody2D> ().gravityScale = 3;
			GetComponent<Rigidbody2D>().AddForce (vector, ForceMode2D.Impulse);
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			opacity = 0;
		}

		void Update() 
		{
			if (damageText.color.a <= 0.1f) {
				Destroy (gameObject);
			} else {
				Color color = damageText.color;
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