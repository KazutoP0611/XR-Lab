using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Runtime.CompilerServices;

public class RepositionEventTriggerDetector : TriggerDetector
{
    public Toggle RepositionButton;

    [Header("Event Settings")]
    public UnityEvent OnClickedEvent;

    public override void InitObjectDetector(Action SentEventTriggered)
    {
        base.InitObjectDetector(SentEventTriggered);

        RepositionButton.onValueChanged.AddListener(x =>
            {
                OnClickedEvent?.Invoke();
                SentEventTriggered?.Invoke();
            });
    }
}
