using UnityEngine;
namespace Unitilities
{
    public class TransformLookAtMoveDirection : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Rotation speed in degrees per second.")]
        public float rotationSpeed = 360;

        [Header("State")]
        public bool look;
        Vector3 lastPosition;

        void LateUpdate()
        {
            if (!look)
            {
                return;
            }
            var direction = transform.position - lastPosition;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            lastPosition = transform.position;
        }

        public void SetLooking(bool value)
        {
            look = value;
        }
    }
}