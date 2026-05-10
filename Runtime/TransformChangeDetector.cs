using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    public class TransformChangeDetector : MonoBehaviour
    {
        [Header("Settings")]
        public int frameDelay;

        [Header("Events")]
        public UnityEvent onMove;
        public UnityEvent onScale;
        public UnityEvent onRotate;

        [Header("State")]
        [Tooltip("Is transform detection active?")]
        public bool detect;

        int frameCount = 0;
        Vector3 lastPosition;
        Vector3 lastScale;
        Quaternion lastRotation;

        void Start()
        {
            lastPosition = transform.position;
            lastScale = transform.localScale;
            lastRotation = transform.rotation;
        }

        void LateUpdate()
        {
            if (!detect)
            {
                return;
            }
            Detect();
        }

        void Detect()
        {
            if (frameCount < frameDelay)
            {
                frameCount++;
                return;
            }
            else
            {
                frameCount = 0;
            }
            if (transform.position != lastPosition)
            {
                lastPosition = transform.position;
                onMove?.Invoke();
            }
            if (transform.localScale != lastScale)
            {
                lastScale = transform.localScale;
                onScale?.Invoke();
            }
            if (transform.rotation != lastRotation)
            {
                lastRotation = transform.rotation;
                onRotate?.Invoke();
            }
        }

        public void SetDetecting(bool value)
        {
            detect = value;
            if (detect)
            {
                frameCount = 0;
            }
        }
    }
}