using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    public class TransformClampPosition : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Clamp in local or world space.")]
        public Space space;

        public bool clampX = true;
        public bool clampY = true;
        public bool clampZ = true;

        [Tooltip("Minimum position coordiantes.")]
        public Vector3 minPosition;

        [Tooltip("Maximum position coordiantes.")]
        public Vector3 maxPosition;

        [Header("Events")]
        public UnityEvent onPositionClamped;

        [Header("State")]
        [Tooltip("Is position clamping active?")]
        public bool clamp;

        void LateUpdate()
        {
            if (!clamp)
            {
                return;
            }

            ClampPosition();
        }

        void ClampPosition()
        {
            var position = space == Space.Self ? transform.localPosition : transform.position;
            var originalPosition = position;
            if (clampX)
            {
                position.x = Mathf.Clamp(
                    position.x,
                    minPosition.x,
                    maxPosition.x
                );
            }

            if (clampY)
            {
                position.y = Mathf.Clamp(
                    position.y,
                    minPosition.y,
                    maxPosition.y
                );
            }

            if (clampZ)
            {
                position.z = Mathf.Clamp(
                    position.z,
                    minPosition.z,
                    maxPosition.z
                );
            }

            if (position != originalPosition)
            {
                if (space == Space.Self)
                {
                    transform.localPosition = position;
                }
                else
                {
                    transform.position = position;
                }

                onPositionClamped?.Invoke();
            }
        }

        public void SetMinPosition(Vector3 minPosition)
        {
            this.minPosition = minPosition;
        }

        public void SetMaxPosition(Vector3 maxPosition)
        {
            this.maxPosition = maxPosition;
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