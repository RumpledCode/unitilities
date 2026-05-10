using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Unitilities
{
    [CustomEditor(typeof(AudioSequence))]
    [CanEditMultipleObjects]
    public class AudioSequenceEditor : Editor
    {
        AudioSequence audioSequence;

        FieldInfo playingField;
        FieldInfo indexField;
        FieldInfo currentElementField;

        void OnEnable()
        {
            audioSequence = (AudioSequence)target;

            var type = typeof(AudioSequence);

            playingField = type.GetField("playing",
                BindingFlags.NonPublic | BindingFlags.Instance);

            indexField = type.GetField("index",
                BindingFlags.NonPublic | BindingFlags.Instance);

            currentElementField = type.GetField("currentElement",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void OnInspectorGUI()
        {
            if (audioSequence == null)
                audioSequence = (AudioSequence)target;

            EditorGUILayout.HelpBox(
                "Plays an ordered sequence of audio clips with playback controls.",
                MessageType.Info);

            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Readonly State", EditorStyles.boldLabel);

            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.Toggle(
                new GUIContent("Playing", "Is the sequence currently playing?"),
                GetPlaying(audioSequence));

            EditorGUILayout.IntField(
                new GUIContent("Index", "Current track index."),
                GetIndex(audioSequence));
            EditorGUILayout.Space();
            AudioSequenceElement element = GetCurrentElement(audioSequence);
            EditorGUILayout.LabelField("Current sequence element", EditorStyles.boldLabel);
            if (element != null)
            {
                EditorGUILayout.ObjectField(
                    new GUIContent("Clip", "Current audio clip."),
                    element.audioClip,
                    typeof(AudioClip),
                    false);

                EditorGUILayout.Slider(
                    new GUIContent("Volume", "Current track volume."),
                    element.volume,
                    0f,
                    1f);

                EditorGUILayout.FloatField(
                    new GUIContent("Pre Delay", "Delay before playback (in seconds)."),
                    element.preDelay);
            }
            else
            {
                EditorGUILayout.LabelField("No active track");
            }

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Playback Controls", EditorStyles.boldLabel);

            GUI.enabled = Application.isPlaying;

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("▶ Start"))
            {
                audioSequence.ReplaySequence();
            }

            if (GUILayout.Button("⏸ Pause"))
            {
                audioSequence.PauseSequence();
            }

            if (GUILayout.Button("▶ Resume"))
            {
                audioSequence.ResumeSequence();
            }

            if (GUILayout.Button("⏹ Stop"))
            {
                audioSequence.StopSequence();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("⏮ Previous"))
            {
                audioSequence.PreviousTrack();
            }

            if (GUILayout.Button("⏭ Next"))
            {
                audioSequence.NextTrack();
            }

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox(
                    "Playback controls are only available in Play Mode.",
                    MessageType.Warning);
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(audioSequence);
            }
        }

        bool GetPlaying(AudioSequence t)
        {
            return (bool)playingField.GetValue(t);
        }

        int GetIndex(AudioSequence t)
        {
            return (int)indexField.GetValue(t);
        }

        AudioSequenceElement GetCurrentElement(AudioSequence t)
        {
            return (AudioSequenceElement)currentElementField.GetValue(t);
        }
    }
}