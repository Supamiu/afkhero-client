using UnityEngine;

namespace AFKHero.Core.Affix
{
    public abstract class AffixImpl
    {
        public abstract void Attach(GameObject go, float value);

        public abstract void Detach();
    }
}
