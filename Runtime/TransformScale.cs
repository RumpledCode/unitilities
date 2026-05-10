using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    public class TransformScale : MonoBehaviour
    {
        [Header("Settings")]
        public Vector3 startScale = Vector3.one;
        public Vector3 endScale = Vector3.one * 2f;
        public float speed = 1f;

        [Header("Events")]
        public UnityEvent targetScaleReached;
        public UnityEvent startScaleReached;

        [Header("State")]
        [Tooltip("Is the scaling active")]
        public bool scale;

        float t = 0f;
        int direction = 1;

        void Update()
        {
            if (!scale)
            {
                return;
            }
            Scale();
        }

        void Scale()
        {
            t += Time.deltaTime * speed * direction;
            if (t >= 1)
            {
                t = 1;
                direction = -1;
                targetScaleReached?.Invoke();
            }
            else if (t <= 0f)
            {
                t = 0f;
                direction = 1;
                startScaleReached?.Invoke();
            }
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
        }

        public void SetScaling(bool value)
        {
            scale = value;
        }

    }
}