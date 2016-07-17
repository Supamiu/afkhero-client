using UnityEngine;
using System.Collections;

namespace AFKHero.Model
{
    public abstract class Wearable : MonoBehaviour
    {
        public abstract void Attach(GameObject o);

        public abstract void Detach();
    }
}
