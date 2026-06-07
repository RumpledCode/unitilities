using UnityEngine;

namespace Unitilities
{
    public class GameObjectUtilities : MonoBehaviour
    {
        [Header("Data")]
        public string debugLogText;

        public void DebugLog(string text)
        {
            Debug.Log(text);
        }

        public void DebugLog()
        {
            DebugLog(debugLogText);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void DestroyImmediate()
        {
            DestroyImmediate(gameObject);
        }
    }
}