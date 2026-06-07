using UnityEditor;
namespace Unitilities
{
    [CustomEditor(typeof(OnCollisionInvokeAction))]
    [CanEditMultipleObjects]
    public class OnCollisionInvokeActionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "Invokes a Unity Event on collision enter, exit or stay. Optional conditioning.",
                MessageType.Info);

            DrawDefaultInspector();
        }
    }
}