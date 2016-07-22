using UnityEngine;
using UnityEngine.UI;
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
        Hide();
	}

	void FixedUpdate ()
	{
		if (!displayed) {
			if (elapsed < cooldown) {
                elapsed += Time.fixedDeltaTime;
			} else {
				string newText = PercentageUtils.Instance.GetRandomItem<string> (entries);
				while (newText == displayText.text) {
					newText = PercentageUtils.Instance.GetRandomItem<string> (entries);
				}
                displayText.text = newText;
                displayImage.gameObject.SetActive (true);
                displayText.gameObject.SetActive (true);
                elapsed = 0f;
                displayed = true;
			}
		} else {
            elapsed += Time.fixedDeltaTime;
			if (elapsed >= duration) {
                Hide();
                elapsed = 0f;
			}
		}
	}

	void Hide ()
	{
        displayText.gameObject.SetActive (false);
        displayImage.gameObject.SetActive (false);
        displayed = false;
	}
}
