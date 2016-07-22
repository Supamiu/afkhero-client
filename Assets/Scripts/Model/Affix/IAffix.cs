using UnityEngine;

namespace AFKHero.Model.Affix
{
    public interface IAffix
    {
        void OnAttach(GameObject go);

        void OnDetach();

        void Roll();
    }
}