using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Unitilities
{
    [CustomEditor(typeof(TransformSmoothLookAt))]
    [CanEditMultipleObjects]
    public class TransformSmoothLookAtEditor : Editor
    {
        TransformSmoothLookAt lookAtTarget;
        FieldInfo lookingDirectlyAtTargetField;

        void OnEnable()
        {
            lookAtTarget = (TransformSmoothLookAt)target;

            lookingDirectlyAtTargetField = typeof(TransformSmoothLookAt)
                .GetField("lookingDirectlyAtTarget",
                    BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void OnInspectorGUI()
        {
            if (lookAtTarget == null)
                lookAtTarget = (TransformSmoothLookAt)target;

            EditorGUILayout.HelpBox(
                "Smoothly looks at a given transform.",
                MessageType.Info);

            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Readonly State", EditorStyles.boldLabel);

            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.Toggle(
                new GUIContent(
                    "Looking Directly At Target",
                    "Is the object currently aligned with the target direction."
                ),
                GetLookingDirectlyAtTarget(lookAtTarget)
            );

            EditorGUI.EndDisabledGroup();
        }

        bool GetLookingDirectlyAtTarget(TransformSmoothLookAt t)
        {
            if (lookingDirectlyAtTargetField == null || t == null)
                return false;

            return (bool)lookingDirectlyAtTargetField.GetValue(t);
        }
    }
}