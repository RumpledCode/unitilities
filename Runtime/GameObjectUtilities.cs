using UnityEngine;

namespace Unitilities
{
    public class GameObjectUtilities : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void DestroyImmediate()
        {
            DestroyImmediate(gameObject);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }
    }
}