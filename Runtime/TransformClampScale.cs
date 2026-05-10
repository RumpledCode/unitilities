using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    public class TransformClampScale : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Clamp scale in local space.")]
        public bool useLocalSpace = true;

        public bool clampX = true;
        public bool clampY = true;
        public bool clampZ = true;

        [Tooltip("Minimum scale values.")]
        public Vector3 minScale;

        [Tooltip("Maximum scale values.")]
        public Vector3 maxScale;

        [Header("Events")]
        public UnityEvent onScaleClamped;

        [Header("State")]
        [Tooltip("Is scale clamping active?")]
        public bool clamp;

        void LateUpdate()
        {
            if (!clamp)
            {
                return;
            }

            ClampScale();
        }

        void ClampScale()
        {
            var scale = useLocalSpace ? transform.localScale : transform.lossyScale;
            var originalScale = scale;

            if (clampX)
            {
                scale.x = Mathf.Clamp(scale.x, minScale.x, maxScale.x);
            }

            if (clampY)
            {
                scale.y = Mathf.Clamp(scale.y, minScale.y, maxScale.y);
            }

            if (clampZ)
            {
                scale.z = Mathf.Clamp(scale.z, minScale.z, maxScale.z);
            }

            if (scale != originalScale)
            {
                transform.localScale = scale;
                onScaleClamped?.Invoke();
            }
        }

        public void SetMinScale(Vector3 minScale)
        {
            this.minScale = minScale;
        }

        public void SetMaxScale(Vector3 maxScale)
        {
            this.maxScale = maxScale;
        }

        public void SetXClamping(bool clampX)
        {
            this.clampX = clampX;
        }

        public void SetYClamping(bool clampY)
        {
            this.clampY = clampY;
        }

        public void SetZClamping(bool clampZ)
        {
            this.clampZ = clampZ;
        }

        public void SetClamping(bool value)
        {
            clamp = value;
        }
    }
}