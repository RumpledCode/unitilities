using UnityEngine;

namespace Unitilities
{
    public class CameraDistanceBasedFov : MonoBehaviour
    {
        [Header("References")]
        public new Camera camera;
        public Transform target;

        [Header("Settings")]
        [Tooltip("The nearest distance for MAXIMUM fov.")]
        public float nearDistance = 4f;
        [Tooltip("The furthest distance for MINIMUM fov.")]
        public float farDistance = 128f;
        [Tooltip("This the fov of the camera when the target is closer or as close to near distance.")]
        public float maxFov = 90f;
        [Tooltip("This the fov of the camera when the target is further than or as far as far distance.")]
        public float minFov = 15f;
        [Tooltip("How fast the fov changes over time. Degrees per second.")]
        public float adjustSpeed = 5f;

        [Header("State")]
        [Tooltip("Is the fov adjustment active")]
        public bool adjustFov;

        void LateUpdate()
        {
            if (camera == null || target == null || !adjustFov)
            {
                return;
            }

            var distance = Vector3.Distance(camera.transform.position, target.position);
            var t = Mathf.InverseLerp(nearDistance, farDistance, distance);
            var targetFov = Mathf.Lerp(maxFov, minFov, t);
            camera.fieldOfView = Mathf.Lerp(
                camera.fieldOfView,
                targetFov,
                Time.deltaTime * adjustSpeed
            );
        }

        public void SetCamera(Camera value)
        {
            camera = value;
        }

        public void SetTarget(Transform value)
        {
            target = value;
        }

        public void SetNearDistance(float value)
        {
            nearDistance = value;
        }

        public void SetFarDistance(float value)
        {
            farDistance = value;
        }

        public void SetDistanceRange(float near, float far) // float wherever you are
        {
            nearDistance = near;
            farDistance = far;
        }

        public void SetMaxFov(float value)
        {
            maxFov = value;
        }

        public void SetMinFov(float value)
        {
            minFov = value;
        }

        public void SetFovRange(float min, float max)
        {
            minFov = min;
            maxFov = max;
        }

        public void SetAdjustSpeed(float value)
        {
            adjustSpeed = value;
        }

        public void SetAdjustFoV(bool value)
        {
            adjustFov = value;
        }
    }
}