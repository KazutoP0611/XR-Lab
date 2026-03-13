using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractor : Interactor
{
    public TriggerDetector TriggerDetector;
    public GameObject ShowObject;

    private void Awake()
    {
        if (ShowObject)
            ShowObject.SetActive(false);
    }

    public override void Init(Action SentOnInteractEvent)
    {
        base.Init(SentOnInteractEvent);

        if (ShowObject)
            ShowObject.SetActive(true);
        TriggerDetector.InitObjectDetector(OnInteracted);
    }

    public override void OnInteracted()
    {
        if (!interacted)
        {
            interacted = true;

            IntructionPanel.SetOnCloseAnimationEndEvent(() =>
            {
                //SetActiveInteractObjects(false);
                OnInteractEndEvent?.Invoke();
            });

            StartCoroutine(WaitForSeconds(WaitForSecsBeforeClosePanel, IntructionPanel.ClosePanel));
        }
    }
}
