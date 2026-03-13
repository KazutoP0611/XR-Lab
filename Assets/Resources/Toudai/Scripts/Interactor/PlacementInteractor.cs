using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementInteractor : Interactor
{
    [Header("Detector Settings")]
    public PlaceObjectDetector ObjectDetector;
    public Transform DetectorTransform;

    [Header("Object Settings")]
    public bool CloseExpectedObjectWhenInteracted = true;
    public GameObject ExpectedObject;

    public void Awake()
    {
        SetActiveInteractObjects(false);
        //ObjectDetector.gameObject.SetActive(false);
    }

    public override void Init(Action SentOnInteractEvent)
    {
        base.Init(SentOnInteractEvent);

        ObjectDetector.transform.position = DetectorTransform.position;
        ObjectDetector.transform.rotation = DetectorTransform.rotation;

        //ObjectDetector.gameObject.SetActive(true);
        SetActiveInteractObjects(true);
        ObjectDetector.InitObjectDetector(ExpectedObject, OnInteracted);
    }

    public override void OnInteracted()
    {
        if (!interacted)
        {
            interacted = true;
            IntructionPanel.SetOnCloseAnimationEndEvent(() =>
            {
                //SetActiveInteractObjects(false);
                if (CloseExpectedObjectWhenInteracted)
                    ExpectedObject.SetActive(false);
                ObjectDetector.gameObject.SetActive(false);

                OnInteractEndEvent?.Invoke();
            });

            StartCoroutine(WaitForSeconds(WaitForSecsBeforeClosePanel, IntructionPanel.ClosePanel));
        }
    }

    public void SetActiveInteractObjects(bool active)
    {
        ObjectDetector.gameObject.SetActive(active);
        ExpectedObject.SetActive(active);
    }
}
