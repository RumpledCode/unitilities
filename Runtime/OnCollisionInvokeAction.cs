using UnityEngine;
using UnityEngine.Events;

namespace Unitilities
{
    [RequireComponent(typeof(Collider))]
    public class OnCollisionInvokeAction : MonoBehaviour
    {
        [Header("Settings")]
        public bool checkOnEnter;
        public bool checkOnExit;
        public bool checkOnStay;

        [Header("Conditions")]
        [Tooltip("Require the colliding object's tag to match before invoking the event.")]
        public bool checkTag;

        [Tooltip("The object tag required to invoke the event.")]
        public string tagValue;

        [Tooltip("Require the colliding object's name to match before invoking the event.")]
        public bool checkName;

        [Tooltip("The object name required to invoke the event.")]
        public string nameValue;

        [Header("Events")]
        public UnityEvent onEnter;
        public UnityEvent onStay;
        public UnityEvent onExit;

        private void OnCollisionEnter(Collision collision)
        {
            if (!checkOnEnter)
            {
                return;
            }

            if (IsColliderValid(collision))
            {
                onEnter?.Invoke();
            }
        }

        private void OnCollisionExit(Collision collision)
        {

            if (!checkOnExit)
            {
                return;
            }
            if (IsColliderValid(collision))
            {
                onExit?.Invoke();
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!checkOnStay)
            {
                return;
            }

            if (IsColliderValid(collision))
            {
                onStay?.Invoke();
            }
        }

        private bool IsColliderValid(Collision collision)
        {
            if (checkTag && !collision.gameObject.CompareTag(tagValue))
            {
                return false;
            }

            if (checkName && !collision.gameObject.name.Equals(nameValue))
            {
                return false;
            }

            return true;
        }

        public void SetCheckOnEnter(bool value)
        {
            checkOnEnter = value;
        }

        public void SetCheckOnExit(bool value)
        {
            checkOnExit = value;
        }

        public void SetCheckOnStay(bool value)
        {
            checkOnStay = value;
        }

        public void SetCheckName(bool value)
        {
            checkName = value;
        }

        public void SetNameValue(string value)
        {
            nameValue = value;
        }

        public void SetCheckTag(bool value)
        {
            checkTag = value;
        }

        public void SetTagValue(string value)
        {
            tagValue = value;
        }
    }
}