using UnityEditor;
using UnityEngine;
using System.Reflection;
namespace Unitilities
{
    [CustomEditor(typeof(DelayedInvokeAction))]
    [CanEditMultipleObjects]
    public class DelayedInvokeActionEditor : Editor
    {
        DelayedInvokeAction delayedInvokeAction;
        FieldInfo remainingTimeField;
        void OnEnable()
        {
            delayedInvokeAction = (DelayedInvokeAction)target;
            remainingTimeField = typeof(DelayedInvokeAction).GetField("remainigTime", BindingFlags.NonPublic | BindingFlags.Instance);
            EditorApplication.update += OnEditorUpdate;
        }
        void OnDisable()
        {
            EditorApplication.update -= OnEditorUpdate;
        }
        void OnEditorUpdate()
        {
            if (Application.isPlaying)
                Repaint();
        }
        public override void OnInspectorGUI()
        {
            if (delayedInvokeAction == null)
            {
                delayedInvokeAction = (DelayedInvokeAction)target;
            }
            EditorGUILayout.HelpBox("Invokes a UnityEvent after a configurable delay. Optionally loops.", MessageType.Info);
            DrawDefaultInspector();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Readonly State", EditorStyles.boldLabel);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.FloatField(
                new GUIContent(
                    "Remaining Time",
                    "Time remaining before the action is invoked."
                ),
                GetRemainingTime(delayedInvokeAction)
            );
            EditorGUI.EndDisabledGroup();
        }
        float GetRemainingTime(DelayedInvokeAction t)
        {
            if (remainingTimeField == null || t == null)
            {
                return 0;
            }
            return (float)remainingTimeField.GetValue(t);
        }
    }
}