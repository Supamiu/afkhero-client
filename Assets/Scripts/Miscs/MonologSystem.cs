using UnityEngine;
using UnityEngine.UI;
using AFKHero.Tools;

public class MonologSystem : MonoBehaviour
{
    public string[] entries =
    {
        "Ma tatan, elle fait des flans.",
        "J'suis chef de guerre comme disent vos romains.",
        "Pour savoir d'où vient le vent, faut mettre son doigt dans l'cul du coq.",
        "On arrive bientôt?",
        "Elle est où la poulette?",
        "Viens m'le dire de profil si t'es un homme.",
        "Le lion ne s'associe pas avec le cafard.",
        "Je tue des trucs, j'suis un tueur"
    };

    [Header("Temps d'affichage de chaque bulle")] public float duration;

    [Header("Temps entre l'affichage de deux bulles")] public float cooldown;

    public Text displayText;

    public Image displayImage;

    private bool displayed;

    private float elapsed;

    private void Start()
    {
        Hide();
    }

    private void FixedUpdate()
    {
        if (!displayed)
        {
            if (elapsed < cooldown)
            {
                elapsed += Time.fixedDeltaTime;
            }
            else
            {
                var newText = PercentageUtils.Instance.GetRandomItem(entries);
                while (newText == displayText.text)
                {
                    newText = PercentageUtils.Instance.GetRandomItem(entries);
                }
                displayText.text = newText;
                displayImage.gameObject.SetActive(true);
                displayText.gameObject.SetActive(true);
                elapsed = 0f;
                displayed = true;
            }
        }
        else
        {
            elapsed += Time.fixedDeltaTime;
            if (!(elapsed >= duration)) return;
            Hide();
            elapsed = 0f;
        }
    }

    private void Hide()
    {
        displayText.gameObject.SetActive(false);
        displayImage.gameObject.SetActive(false);
        displayed = false;
    }
}