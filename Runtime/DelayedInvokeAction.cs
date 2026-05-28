using UnityEngine;
using UnityEngine.Events;

public class DelayedInvokeAction : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The time in second after which the UnityEvent will be invoked.")]
    public float delayTime;
    [Tooltip("Should the delay be looped?")]
    public bool loop;

    [Header("Action")]
    public UnityEvent action;

    [Header("State")]
    [Tooltip("Is the timer active and counting down to 0.")]
    public bool timerActive;
    [Tooltip("How much time in seconds is left until the event is invoked.")]
    float remainigTime;

    void Awake()
    {
        remainigTime = delayTime;
    }

    void Update()
    {
        if (timerActive)
        {
            remainigTime -= Time.deltaTime;
            if (remainigTime <= 0)
            {
                timerActive = loop;
                remainigTime = delayTime;
                action?.Invoke();
            }
        }
    }

    public void SetDelayTime(float value)
    {
        delayTime = value;
    }

    public void SetLoop(bool value)
    {
        loop = value;
    }

    public void SetAction(UnityEvent value)
    {
        action = value;
    }

    public float GetRemainingTime()
    {
        return remainigTime;
    }
}
