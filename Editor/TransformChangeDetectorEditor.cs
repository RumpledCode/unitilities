using UnityEditor;

namespace Unitilities
{
    [CustomEditor(typeof(TransformChangeDetector))]
    [CanEditMultipleObjects]
    public class TransformChangeDetectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "Invokes a Unity event when the attached Transform moves, rotates or scales. Processed in LateUpdate().",
                MessageType.Info);

            DrawDefaultInspector();
        }
    }
}