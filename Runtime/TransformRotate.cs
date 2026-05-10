using UnityEngine;

namespace Unitilities
{
    public class TransformRotate : MonoBehaviour
    {
        [Header("Settings")]
        public float xRotationSpeed = 0f;
        public float yRotationSpeed = 0f;
        public float zRotationSpeed = 0f;

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
            transform.Rotate(
                xRotationSpeed * Time.deltaTime,
                yRotationSpeed * Time.deltaTime,
                zRotationSpeed * Time.deltaTime,
                Space.Self
            );
        }

        public void SetRotating(bool value)
        {
            rotate = value;
        }
    }
}