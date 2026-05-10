using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    public class AudioSequence : MonoBehaviour
    {
        [Header("References")]
        public AudioSource audioSource;

        [Header("Settings")]
        public bool autoplay;
        public bool loop;

        [Header("Data")]
        public List<AudioSequenceElement> sequence;

        [Header("State")]
        bool playing;
        AudioSequenceElement currentElement;
        int index;

        public void PauseSequence()
        {
            audioSource.Pause();
            playing = false;
        }

        public void StopSequence()
        {
            audioSource.Stop();
            playing = false;
            ChangeTrack(0);
        }

        public void ResumeSequence()
        {
            if (audioSource.time > 0f)
            {
                audioSource.UnPause();
            }
            else
            {
                PlayCurrent();
            }
            playing = true;
        }

        public void ReplaySequence()
        {
            playing = true;
            ChangeTrack(0);
        }

        public void NextTrack()
        {

            ChangeTrack((index + 1) % sequence.Count);
        }

        public void PreviousTrack()
        {
            ChangeTrack((index - 1 + sequence.Count) % sequence.Count);
        }

        public void ChangeTrack(int i)
        {
            if (sequence == null || sequence.Count == 0)
            {
                return;
            }
            index = i;
            currentElement = sequence[index];
            audioSource.clip = currentElement.audioClip;
            if (playing)
            {
                PlayCurrent();
            }
        }

        void CheckTrackEnd()
        {
            if (playing && !audioSource.isPlaying)
            {
                currentElement.onFinished?.Invoke();
                if (index == sequence.Count - 1 && !loop)
                {
                    playing = false;
                    return;
                }
                else
                {
                    NextTrack();
                }
            }
        }

        void PlayCurrent()
        {
            audioSource.volume = currentElement.volume;
            audioSource.PlayDelayed(currentElement.preDelay);
            currentElement.onStarted?.Invoke();
        }

        void Start()
        {
            if (autoplay)
            {
                ReplaySequence();
            }
        }

        void Update()
        {
            if (audioSource == null || sequence == null || sequence.Count == 0)
            {
                return;
            }
            CheckTrackEnd();
        }

        [Serializable]
        public class AudioSequenceElement
        {
            public AudioClip audioClip;
            [Range(0.0f, 1.0f)]
            public float volume = 1.0f;
            [Tooltip("Delay before playback (in seconds).")]
            public float preDelay;
            [Tooltip("Called when the audio clip starts playing.")]
            public UnityEvent onStarted;
            [Tooltip("Called when the audio clip finishes playing. Does not get invoked when track is changed before it finished playing.")]
            public UnityEvent onFinished;
        }
    }
}