using UnityEditor;

namespace Unitilities
{
    [CustomEditor(typeof(CameraScreenPositioner))]
    [CanEditMultipleObjects]
    public class CameraScreenPositionerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "Sets cameras to the respective selected position on screen.",
                MessageType.Info);

            DrawDefaultInspector();
        }
    }
}