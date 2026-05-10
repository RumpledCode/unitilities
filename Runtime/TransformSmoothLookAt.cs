using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    public class TransformSmoothLookAt : MonoBehaviour
    {
        [Header("References")]
        public Transform target;

        [Header("Settings")]
        [Tooltip("Degrees per second")]
        public float rotationSpeed = 1.0f;

        [Tooltip("Anything under this angle (in degrees) will be treated as if the object is looking straight at the target.")]
        [Range(0.0f, 180f)]
        public float angleThreshold = 1f;

        [Header("Events")]
        public UnityEvent onTargetLookedDirectly;

        [Header("State")]
        [Tooltip("Is the smooth look at active?")]
        public bool lookAt;

        [Tooltip("Is the object looking directly at the target?")]
        bool lookingDirectlyAtTarget;

        void Update()
        {
            if (!lookAt || target == null)
            {
                return;
            }
            SmoothLookAt();
        }

        void SmoothLookAt()
        {
            var direction = target.position - transform.position;
            var angle = Vector3.Angle(transform.forward, direction);
            if (angle < angleThreshold)
            {
                if (!lookingDirectlyAtTarget)
                {
                    onTargetLookedDirectly?.Invoke();
                }
                lookingDirectlyAtTarget = true;
                return;
            }
            lookingDirectlyAtTarget = false;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void SetLooking(bool value)
        {
            lookAt = value;
        }

        public void StopLookingAt()
        {
            lookAt = false;
        }

        public void ToggleLookingAt()
        {
            lookAt = !lookAt;
        }

        public bool IsLookingDirectlyAtTarget()
        {
            return lookingDirectlyAtTarget;
        }

    }
}