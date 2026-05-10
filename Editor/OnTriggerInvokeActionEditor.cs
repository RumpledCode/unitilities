using UnityEditor;
namespace Unitilities
{
    [CustomEditor(typeof(OnTriggerInvokeAction))]
    [CanEditMultipleObjects]
    public class OnTriggerInvokeActionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox(
                "Invokes a Unity Event on trigger enter, exit or stay. Optional conditioning.",
                MessageType.Info);

            DrawDefaultInspector();
        }
    }
}