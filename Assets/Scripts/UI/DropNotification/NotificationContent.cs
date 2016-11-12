using UnityEngine;

namespace AFKHero.UI.DropNotification
{
    public class NotificationContent : MonoBehaviour
    {
        public WearableDropNotification notif;

        public void EndAnimation()
        {
            notif.Remove();
        }
    }
}
