using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction;

public class TriggerDetector : MonoBehaviour
{
    private Action OnEventTriggered;

    public virtual void InitObjectDetector(Action SentEventTriggered)
    {
        OnEventTriggered = SentEventTriggered;
    }

    public virtual void OnTriggerInteracted()
    {
        OnEventTriggered?.Invoke();
    }
}
