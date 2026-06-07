using System.Collections.Generic;
using UnityEngine;

namespace Unitilities
{
    [RequireComponent(typeof(Collider))]
    public class TriggerCounter : MonoBehaviour
    {

        [Header("State")]
        readonly HashSet<Collider> insideTriggerVolume = new();

        [Header("Conditions")]
        [Tooltip("Require the colliding object's tag to match before invoking the event.")]
        public bool checkTag;

        [Tooltip("The object tag required to invoke the event.")]
        public string tagValue = "";

        [Tooltip("Require the colliding object's name to match before invoking the event.")]
        public bool checkName;

        [Tooltip("The object name required to invoke the event.")]
        public string nameValue = "";

        private void OnTriggerEnter(Collider collider)
        {
            if (IsTriggerValid(collider))
            {
                insideTriggerVolume.Add(collider);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            insideTriggerVolume.Remove(collider);
        }

        void FixedUpdate()
        {
            insideTriggerVolume.RemoveWhere(IsNoLongerActive);
        }

        private bool IsNoLongerActive(Collider collider)
        {
            return collider == null || !collider.gameObject.activeInHierarchy;
        }

        private bool IsTriggerValid(Collider collider)
        {
            if (checkTag && !collider.CompareTag(tagValue))
            {
                return false;
            }

            if (checkName && !collider.name.Equals(nameValue))
            {
                return false;
            }

            return true;
        }

        public int GetCount()
        {
            return insideTriggerVolume.Count;
        }
    }
}
