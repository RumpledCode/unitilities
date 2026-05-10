using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Unitilities
{
    [CustomEditor(typeof(TransformSmoothFollow))]
    [CanEditMultipleObjects]
    public class TransformSmoothFollowEditor : Editor
    {
        TransformSmoothFollow followTarget;
        FieldInfo atTargetLocationField;

        void OnEnable()
        {
            followTarget = (TransformSmoothFollow)target;

            atTargetLocationField = typeof(TransformSmoothFollow)
                .GetField("atTargetLocation",
                    BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void OnInspectorGUI()
        {
            if (followTarget == null)
                followTarget = (TransformSmoothFollow)target;

            EditorGUILayout.HelpBox(
                "Smoothly follows a given transform.",
                MessageType.Info);

            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Readonly State", EditorStyles.boldLabel);

            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.Toggle(
                new GUIContent(
                    "At Target Location",
                    "Is the target currently at the desired follow position."
                ),
                GetAtTargetLocation(followTarget)
            );

            EditorGUI.EndDisabledGroup();
        }

        bool GetAtTargetLocation(TransformSmoothFollow t)
        {
            if (atTargetLocationField == null || t == null)
                return false;

            return (bool)atTargetLocationField.GetValue(t);
        }
    }
}