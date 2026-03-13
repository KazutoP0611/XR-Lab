using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class ActiveStateEventTriggerDetector : TriggerDetector
{
    [Header("Event Wrapper")]
    public ActiveStateUnityEventWrapper ActiveStateEventWrapper;

    public override void InitObjectDetector(Action SentEventTriggered)
    {
        base.InitObjectDetector(SentEventTriggered);

        if (ActiveStateEventWrapper)
            ActiveStateEventWrapper.enabled = true;
    }
}
