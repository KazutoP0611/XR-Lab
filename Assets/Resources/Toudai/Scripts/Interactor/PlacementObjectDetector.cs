using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class PlaceObjectDetector : MonoBehaviour
{
    public Transform ObjectFinalTransform;
    public GameObject MarkedGhostObject;
    public bool ShowTargetPoint;
    public GameObject TargetPoint;

    protected GameObject ExpectedObject;
    protected Action OnEventTriggered;

    public void InitObjectDetector(GameObject SentExpectedObject, Action SentEventTriggered)
    {
        ExpectedObject = SentExpectedObject;
        OnEventTriggered = SentEventTriggered;

        MarkedGhostObject.SetActive(true);

        if (ShowTargetPoint)
            TargetPoint.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ExpectedObject)
        {
            GrabInteractable grabbable = other.GetComponentInChildren<GrabInteractable>();
            HandGrabInteractable handGrab = other.GetComponentInChildren<HandGrabInteractable>();

            if (grabbable && handGrab)
            {
                //Debug.LogWarning("Hello!!");
                grabbable.enabled = false;
                handGrab.enabled = false;
            }

            TargetPoint.SetActive(false);
            ExpectedObject.transform.position = ObjectFinalTransform.position;
            ExpectedObject.transform.rotation = ObjectFinalTransform.rotation;
            MarkedGhostObject.SetActive(false);

            OnEventTriggered?.Invoke();
        }
    }

    protected virtual void EnableInteractable()
    {
       
    }
}
