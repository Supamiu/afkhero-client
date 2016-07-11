using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AFKHero.Tools;

public class MonologSystem : MonoBehaviour
{

	public string[] entries = {
		"Ma tatan, elle fait des flans.",
		"J'suis chef de guerre comme disent vos romains.",
		"Pour savoir d'où vient le vent, faut mettre son doigt dans l'cul du coq.",
		"On arrive bientôt?",
		"Elle est où la poulette?",
		"Viens m'le dire de profil si t'es un homme.",
		"Le lion ne s'associe pas avec le cafard."
	};

	[Header ("Temps d'affichage de chaque bulle")]
	public float duration = 0f;

	[Header ("Temps entre l'affichage de deux bulles")]
	public float cooldown = 0f;

	public Text displayText;

	public Image displayImage;

	private bool displayed = false;

	private float elapsed = 0f;

	void Start ()
	{
		this.Hide ();
	}

	void FixedUpdate ()
	{
		if (!this.displayed) {
			if (this.elapsed < this.cooldown) {
				this.elapsed += Time.fixedDeltaTime;
			} else {
				string newText = PercentageUtils.Instance.GetRandomItem<string> (this.entries);
				while (newText == this.displayText.text) {
					newText = PercentageUtils.Instance.GetRandomItem<string> (this.entries);
				}
				this.displayText.text = newText;
				this.displayImage.gameObject.SetActive (true);
				this.displayText.gameObject.SetActive (true);
				this.elapsed = 0f;
				this.displayed = true;
			}
		} else {
			this.elapsed += Time.fixedDeltaTime;
			if (this.elapsed >= this.duration) {
				this.Hide ();
				this.elapsed = 0f;
			}
		}
	}

	void Hide ()
	{
		this.displayText.gameObject.SetActive (false);
		this.displayImage.gameObject.SetActive (false);
		this.displayed = false;
	}
}
