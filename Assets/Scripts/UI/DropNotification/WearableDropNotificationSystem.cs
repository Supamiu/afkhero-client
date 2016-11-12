using UnityEngine;
using AFKHero.Core.Event;
using AFKHero.Model;
using AFKHero.Core.Database;
using System.Collections.Generic;
using System;

namespace AFKHero.UI.DropNotification
{
    public class WearableDropNotificationSystem : MonoBehaviour
    {

        public WearableDropNotification notificationPrefab;

        private readonly Queue<Drop> drops = new Queue<Drop>();

        private bool notifying;

        // Use this for initialization
        private void Start()
        {
            EventDispatcher.Instance.Register(Events.DROP, new Listener<GenericGameEvent<Drop>>((ref GenericGameEvent<Drop> e) =>
            {
                drops.Enqueue(e.Data);
            }, -10));
        }

        private void Update()
        {
            if (!notifying && drops.Count > 0)
            {
                Notify();
            }
        }

        private void Notify()
        {
            try
            {
                var drop = drops.Dequeue();
                var w = ItemDatabaseConnector.Instance.GetWearable(drop.itemID);
                var notif = Instantiate(notificationPrefab);
                notif.SetWearable(w);
                notif.transform.SetParent(transform);
                notif.transform.localScale = Vector3.one;
                notif.transform.position = transform.position;
                notifying = true;
                notif.OnRemove += () => { notifying = false; };
            }
            catch (ItemDatabaseConnector.ItemNotFoundException ignored)
            {
                ignored.GetBaseException();
            }
            catch (InvalidOperationException ign)
            {
                //Quand le queue est vide, on a rien � show.
                ign.GetBaseException();
            }
        }
    }
}
