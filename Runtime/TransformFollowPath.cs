using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    public class TransformFollowPath : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Loop: goes from the last stop to the first directly. Reverse: goes back in reverese through all stops.")]
        public RepeatMode repeatMode;
        public float speed = 4;
        public List<PathStop> stops;
        public bool skipFirst;

        [Tooltip("Anything under this distance will be considered as having arrived at the target's position.")]
        public float distanceThreshold = 0.001f;

        [Header("State")]
        public bool move;
        PathStop lastStop;
        PathStop targetStop;
        int index;
        float timeWaited;
        Action updateAction;

        void Start()
        {
            SetStop(skipFirst ? 1 : 0);
        }

        void Update()
        {
            if (!move)
            {
                return;
            }
            updateAction?.Invoke();
        }

        public void SetMoving(bool value)
        {
            move = value;
        }

        void Move()
        {
            var direction = targetStop.transform.position - transform.position;
            if (direction.sqrMagnitude < distanceThreshold)
            {
                NextStop();
                return;
            }
            transform.position += speed * Time.deltaTime * direction.normalized;
        }

        void Wait()
        {
            timeWaited += Time.deltaTime;
            if (lastStop == null || timeWaited >= lastStop.waitTime)
            {
                lastStop?.onDeparted?.Invoke();
                updateAction = Move;
                updateAction.Invoke();
                return;
            }
        }

        void NextStop()
        {
            targetStop.onReached?.Invoke();
            lastStop = targetStop;
            ++index;
            if (index == stops.Count)
            {
                if (repeatMode == RepeatMode.Reverese)
                {
                    stops.Reverse();
                }
                index = 0;
            }
            SetStop(index);
        }

        void SetStop(int index)
        {
            if (stops.Count == 0)
            {
                return;
            }
            targetStop = stops[index];
            timeWaited = 0;
            updateAction = Wait;
            updateAction.Invoke();
        }

        public enum RepeatMode
        {
            Loop,
            Reverese
        }

        [Serializable]
        public class PathStop
        {
            [Tooltip("The target transform to reach.")]
            public Transform transform;
            [Tooltip("Wait time before moving towards the next stop.")]
            public float waitTime;
            [Tooltip("Invoked when this stop is reached.")]
            public UnityEvent onReached;
            [Tooltip("Invoked when this stop is departed (wait time expired).")]
            public UnityEvent onDeparted;
        }
    }
}