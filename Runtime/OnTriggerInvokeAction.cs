using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnTriggerInvokeAction : MonoBehaviour
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

    public void ToggleCheckOnEnter()
    {
        checkOnEnter = !checkOnEnter;
    }

    public void ToggleCheckOnExit()
    {
        checkOnExit = !checkOnExit;
    }

    public void ToggleCheckOnStay()
    {
        checkOnStay = !checkOnStay;
    }

    public void ToggleCheckName()
    {
        checkName = !checkName;
    }

    public void ToggleCheckTag()
    {
        checkTag = !checkTag;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!checkOnEnter)
        {
            return;
        }

        if (IsTriggerValid(collider))
        {
            onEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {

        if (!checkOnExit)
        {
            return;
        }
        if (IsTriggerValid(collider))
        {
            onExit?.Invoke();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!checkOnStay)
        {
            return;
        }

        if (IsTriggerValid(collider))
        {
            onStay?.Invoke();
        }
    }
}