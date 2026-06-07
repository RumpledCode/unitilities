using UnityEditor;
using UnityEngine;

namespace Unitilities
{
    [CustomEditor(typeof(TriggerCounter))]
    [CanEditMultipleObjects]
    public class TriggerCounterEditor : Editor
    {
        TriggerCounter triggerCounter;

        void OnEnable()
        {
            triggerCounter = (TriggerCounter)target;
        }

        public override void OnInspectorGUI()
        {
            if (triggerCounter == null)
            {
                triggerCounter = (TriggerCounter)target;
            }

            EditorGUILayout.HelpBox("Counts how many triggers are currently inside of the object's trigger volume.", MessageType.Info);

            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Readonly State", EditorStyles.boldLabel);

            EditorGUI.BeginDisabledGroup(true);

            EditorGUILayout.IntField(
                new GUIContent("Count", "Number of valid colliders currently inside the trigger volume."),
                triggerCounter.GetCount());

            EditorGUI.EndDisabledGroup();

            if (Application.isPlaying)
            {
                Repaint();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(triggerCounter);
            }
        }
    }
}