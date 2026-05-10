using UnityEditor;

namespace Unitilities
{
    [CustomEditor(typeof(TransformLookAtMoveDirection))]
    [CanEditMultipleObjects]
    public class TransformLookAtMoveDirectionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "Rotates the attached Transform towards the movement direction (calculated based on last two frames).",
                MessageType.Info);

            DrawDefaultInspector();
        }
    }
}