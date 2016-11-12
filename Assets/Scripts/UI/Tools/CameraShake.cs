using UnityEngine;
using AFKHero.Core.Event;

namespace AFKHero.UI.Tools
{
    public class CameraShake : MonoBehaviour
    {
        private Vector3 originPosition;
        private Quaternion originRotation;
        public float shake_decay;
        public float shake_intensity;

        private void OnGUI()
        {
            EventDispatcher.Instance.Register("shake", new Listener<GameEvent>((ref GameEvent e) =>
            {
                Shake();
            }));
        }

        private void Update()
        {
            if (!(shake_intensity > 0)) return;
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
                originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
                originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
            shake_intensity -= shake_decay;
            if (!(shake_intensity <= 0)) return;
            transform.position = originPosition;
            transform.rotation = originRotation;
        }

        private void Shake()
        {
            originPosition = transform.position;
            originRotation = transform.rotation;
            shake_intensity = .2f;
            shake_decay = 0.005f;
        }
    }
}
