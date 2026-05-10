using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    public class TransformBounce : MonoBehaviour
    {
        [Header("Settings")]
        public Vector3 targetPosition;
        public float speed = 1f;

        [Tooltip("Controls speed over time (0–1). X axis = progress, Y axis = speed multiplier")]
        public AnimationCurve speedCurve = AnimationCurve.Linear(0, 1, 1, 1);

        const float zeroThreshold = 0.001f;

        [Header("Events")]
        [Tooltip("UnityEvent invoked when target position is reached.")]
        public UnityEvent targetPositionReached;
        [Tooltip("UnityEvent invoked when starting position is reached.")]
        public UnityEvent startPositionReached;

        [Header("State")]
        [Tooltip("Is the bouncing active")]
        public bool bounce;

        Vector3 originPosition;
        int direction = 1;
        Vector3 currentDestination;

        float t = 0f;

        void Start()
        {
            originPosition = transform.position;
        }

        void Update()
        {
            if (!bounce)
            {
                return;
            }
            Bounce();
        }

        void Bounce()
        {
            currentDestination = direction == 1 ? targetPosition : originPosition;
            t += Time.deltaTime * speed * speedCurve.Evaluate(t) * direction;
            t = Mathf.Clamp01(t);
            transform.position = Vector3.Lerp(originPosition, targetPosition, t);
            if ((transform.position - currentDestination).sqrMagnitude <= zeroThreshold)
            {
                if (direction == 1)
                {
                    targetPositionReached?.Invoke();
                    direction = -1;
                }
                else
                {
                    startPositionReached?.Invoke();
                    direction = 1;
                }
            }
        }

        public void SetBouncing(bool value)
        {
            bounce = value;
        }
    }
}