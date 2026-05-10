using UnityEditor;
namespace Unitilities
{
    [CustomEditor(typeof(GameObjectUtilities))]
    [CanEditMultipleObjects]
    public class GameObjectUtilitiesEditorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "A component that exposes various GameObject related methods so they can be referenced easily in Unity Events.",
                MessageType.Info);

            DrawDefaultInspector();
        }
    }
}