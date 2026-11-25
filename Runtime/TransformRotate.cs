using UnityEngine;

namespace Unitilities
{
    public class TransformRotate : MonoBehaviour
    {
        [Header("Settings")]
        public float xRotationSpeed = 0f;
        public float yRotationSpeed = 0f;
        public float zRotationSpeed = 0f;
        public bool useUnscaledTime = false;

        [Header("State")]
        [Tooltip("Is the rotation active")]
        public bool rotate;

        void Update()
        {
            if (!rotate)
            {
                return;
            }
            Rotate();
        }

        void Rotate()
        {
            var dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.DeltaTime;
            transform.Rotate(
                xRotationSpeed * dt,
                yRotationSpeed * dt,
                zRotationSpeed * dt,
                Space.Self
            );
        }

        public void SetRotating(bool value)
        {
            rotate = value;
        }
    }
}