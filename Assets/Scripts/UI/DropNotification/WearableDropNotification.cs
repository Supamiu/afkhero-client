using UnityEngine;
using UnityEngine.UI;
using AFKHero.Model;
using AFKHero.UI.Tools;

namespace AFKHero.UI.DropNotification
{
    public class WearableDropNotification : MonoBehaviour
    {
        public Animator animator;

        public Image icon;

        public Text itemName;

        public Image border;

        void OnEnable()
        {
            Invoke("Disappear", 1f);
        }

        public void SetWearable(Wearable w)
        {
            icon.sprite = w.icon;
            itemName.text = w.itemName;
            border.color = UITools.GetItemColor(w.rarity);
        }

        void Disappear()
        {
            animator.SetTrigger("Remove");
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}
