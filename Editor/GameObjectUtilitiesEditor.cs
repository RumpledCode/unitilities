using UnityEditor;
namespace Unitilities
{
    [CustomEditor(typeof(GameObjectUtilities))]
    [CanEditMultipleObjects]
    public class GameObjectUtilitiesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "A component that exposes various GameObject and Transform related methods so they can be referenced easily in Unity Events.",
                MessageType.Info);

            DrawDefaultInspector();
        }
    }
}