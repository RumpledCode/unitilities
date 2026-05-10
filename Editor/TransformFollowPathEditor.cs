using System.Reflection;
using UnityEditor;
using UnityEngine;
using static Unitilities.TransformFollowPath;

namespace Unitilities
{
    [CustomEditor(typeof(TransformFollowPath))]
    [CanEditMultipleObjects]
    public class TransformFollowPathEditor : Editor
    {
        TransformFollowPath transformFollowPath;

        FieldInfo index;
        FieldInfo timeWaited;
        FieldInfo lastStop;
        FieldInfo targetStop;

        void OnEnable()
        {
            transformFollowPath = (TransformFollowPath)target;
            var type = typeof(TransformFollowPath);

            index = type.GetField("index",
                BindingFlags.NonPublic | BindingFlags.Instance);

            timeWaited = type.GetField("timeWaited",
            BindingFlags.NonPublic | BindingFlags.Instance);

            lastStop = type.GetField("lastStop",
            BindingFlags.NonPublic | BindingFlags.Instance);

            targetStop = type.GetField("targetStop",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void OnInspectorGUI()
        {
            if (transformFollowPath == null)
            {
                transformFollowPath = (TransformFollowPath)target;
            }

            EditorGUILayout.HelpBox(
                "Follows a predetermined path using a constant speed.",
                MessageType.Info);

            DrawDefaultInspector();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Readonly State", EditorStyles.boldLabel);

            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.IntField(
                new GUIContent("Index", "Current track index."),
                GetIndex(transformFollowPath));

            EditorGUILayout.FloatField(
            new GUIContent("Time Waited", "Time waited at the last stop."),
            GetTimeWaited(transformFollowPath));
            EditorGUILayout.Space();
            PathStop lastStop = GetLastStop(transformFollowPath);
            EditorGUILayout.LabelField("Current sequence element", EditorStyles.boldLabel);
            if (lastStop != null)
            {
                EditorGUILayout.ObjectField(
                    new GUIContent("Transform", "The target transform."),
                    lastStop.transform,
                    typeof(Transform),
                    false);

                EditorGUILayout.FloatField(
                    new GUIContent("Wait Time", "Wait time before moving towards the next stop."),
                    lastStop.waitTime);
            }
            else
            {
                EditorGUILayout.LabelField("No last stop");
            }

            PathStop targetStop = GetTargetStop(transformFollowPath);
            EditorGUILayout.LabelField("Current sequence element", EditorStyles.boldLabel);
            if (targetStop != null)
            {
                EditorGUILayout.ObjectField(
                    new GUIContent("Transform", "The target transform."),
                    targetStop.transform,
                    typeof(Transform),
                    false);

                EditorGUILayout.FloatField(
                    new GUIContent("Wait Time", "Wait time before moving towards the next stop."),
                    targetStop.waitTime);
            }
            else
            {
                EditorGUILayout.LabelField("No target stop");
            }

            EditorGUI.EndDisabledGroup();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(transformFollowPath);
            }
        }

        int GetIndex(TransformFollowPath t)
        {
            return (int)index.GetValue(t);
        }

        float GetTimeWaited(TransformFollowPath t)
        {
            return (float)timeWaited.GetValue(t);
        }

        PathStop GetLastStop(TransformFollowPath t)
        {
            return (PathStop)lastStop.GetValue(t);
        }

        PathStop GetTargetStop(TransformFollowPath t)
        {
            return (PathStop)targetStop.GetValue(t);
        }
    }
}