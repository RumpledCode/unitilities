using UnityEditor;

namespace Unitilities
{
    [CustomEditor(typeof(CameraDistanceBasedFov))]
    [CanEditMultipleObjects]
    public class CameraDistanceBasedFovEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "Changes a camera's field of view based on distance. The closer the object the larger the field of view. The further the object the smaller the field of view.",
                MessageType.Info);

            DrawDefaultInspector();
        }
    }
}