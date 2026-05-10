using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    public class TransformShake : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Shake intensity per axis.")]
        public Vector3 intensity = Vector3.one;

        [Tooltip("How fast the shake changes.")]
        public float frequency = 6f;

        [Tooltip("How quickly the shake fades out.")]
        public float decayIntensity = 1f;

        [Tooltip("Shake in local or world space.")]
        public Space space = Space.World;

        public bool shakeX = true;
        public bool shakeY = true;
        public bool shakeZ = true;

        [Header("Events")]
        public UnityEvent onPositionReset;

        [Header("State")]
        [Tooltip("Is shaking active?")]
        public bool shake;

        Vector3 originalPosition;
        float time;
        float decayFactor = 0f;

        float seedX;
        float seedY;
        float seedZ;

        void Start()
        {
            originalPosition = space == Space.Self ? transform.localPosition : transform.position;
            seedX = Random.value;
            seedY = Random.value;
            seedZ = Random.value;
        }

        void Update()
        {
            Shake();
        }

        void Shake()
        {
            if (!shake)
            {
                if (decayFactor <= 0f)
                {
                    decayFactor = 0f;
                    ResetPosition();
                    return;
                }

                decayFactor -= Time.deltaTime * decayIntensity;
            }
            else
            {
                decayFactor = 1f;
            }

            time += Time.deltaTime * frequency;

            Vector3 offset = new Vector3(
                shakeX ? Mathf.PerlinNoise(seedX, time) * 2f - 1f : 0f,
                shakeY ? Mathf.PerlinNoise(seedY, time) * 2f - 1f : 0f,
                shakeZ ? Mathf.PerlinNoise(seedZ, time) * 2f - 1f : 0f
            );

            offset = Vector3.Scale(offset, intensity) * decayFactor;

            if (space == Space.Self)
            {
                transform.localPosition = originalPosition + offset;
            }
            else
            {
                transform.position = originalPosition + offset;
            }
        }

        void ResetPosition()
        {
            if (space == Space.Self)
            {
                transform.localPosition = originalPosition;
            }
            else
            {
                transform.position = originalPosition;
            }

            onPositionReset?.Invoke();
        }

        public void SetIntensity(Vector3 intensity)
        {
            this.intensity = intensity;
        }

        public void SetFrequency(float frequency)
        {
            this.frequency = frequency;
        }

        public void SetDecayTime(float decayTime)
        {
            this.decayIntensity = decayTime;
        }

        public void StartShaking()
        {
            shake = true;
            decayFactor = 1f;
            time = 0f;
            originalPosition = space == Space.Self ? transform.localPosition : transform.position;
        }

        public void StopShaking()
        {
            shake = false;
        }
    }
}