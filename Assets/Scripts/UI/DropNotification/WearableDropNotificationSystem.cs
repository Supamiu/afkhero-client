using UnityEngine;
using AFKHero.Core.Event;
using AFKHero.Model;
using AFKHero.Core.Database;

namespace AFKHero.UI.DropNotification
{
    public class WearableDropNotificationSystem : MonoBehaviour
    {

        public WearableDropNotification notificationPrefab;

        // Use this for initialization
        void Start()
        {
            EventDispatcher.Instance.Register("drop", new Listener<GenericGameEvent<Drop>>((ref GenericGameEvent<Drop> e) =>
            {
                try
                {
                    Wearable w = ItemDatabaseConnector.Instance.GetWearable(e.Data.itemID);
                    WearableDropNotification notif = Instantiate(notificationPrefab);
                    notif.SetWearable(w);
                    notif.transform.SetParent(transform);
                    notif.transform.localScale = Vector3.one;
                    notif.transform.position = transform.position;
                }
                catch (ItemDatabaseConnector.ItemNotFoundException ignored)
                {
                    ignored.GetBaseException();
                }
            }, -10));
        }
    }
}
