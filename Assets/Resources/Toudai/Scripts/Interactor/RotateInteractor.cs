using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInteractor : Interactor
{
    [Header("Detector Settings")]
    public RotateObjectDetector RotateObjectDetector;
    public Transform DetectorSetPosition;

    [Header("Object Settings")]
    public GameObject ExpectedObject;

    private void Awake()
    {
        SetActiveInteractObjects(false);
    }

    public override void Init(Action SentOnInteractEvent)
    {
        base.Init(SentOnInteractEvent);

        RotateObjectDetector.transform.position = DetectorSetPosition.position;
        RotateObjectDetector.transform.rotation = DetectorSetPosition.rotation;

        SetActiveInteractObjects(true);
        RotateObjectDetector.InitObjectDetector(ExpectedObject, OnInteracted);
    }

    public override void OnInteracted()
    {
        if (!interacted)
        {
            interacted = true;
            IntructionPanel.SetOnCloseAnimationEndEvent(() =>
            {
                SetActiveInteractObjects(false);
                OnInteractEndEvent?.Invoke();
            });

            StartCoroutine(WaitForSeconds(WaitForSecsBeforeClosePanel, IntructionPanel.ClosePanel));
        }
    }

    public void SetActiveInteractObjects(bool active)
    {
        RotateObjectDetector.gameObject.SetActive(active);
        ExpectedObject.SetActive(active);
    }
}
