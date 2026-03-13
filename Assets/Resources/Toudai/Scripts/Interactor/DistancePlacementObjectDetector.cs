using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class DistancePlacementObjectDetector : PlaceObjectDetector
{
    [Header("Hand Grab Interactors")]
    public HandGrabInteractor LeftHandGrabInteractor;
    public HandGrabInteractor RightHandGrabInteractor;
    public HandGrabInteractor LeftHandControllerGrabInteractor;
    public HandGrabInteractor RightHandControllerGrabInteractor;

    [Header("Distance Grab Interactors")]
    public DistanceHandGrabInteractor LeftDistanceHandGrabInteractor;
    public DistanceHandGrabInteractor RightDistanceHandGrabInteractor;
    public DistanceHandGrabInteractor LeftDistanceHandControllerGrabInteractor;
    public DistanceHandGrabInteractor RightDistanceHandControllerGrabInteractor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ExpectedObject)
        {
            //GrabInteractable grabbable = other.GetComponentInChildren<GrabInteractable>();
            //HandGrabInteractable handGrab = other.GetComponentInChildren<HandGrabInteractable>();
            //DistanceGrabInteractable distanceGrab = other.GetComponentInChildren<DistanceGrabInteractable>();
            //DistanceHandGrabInteractable distanceHandGrab = other.GetComponentInChildren<DistanceHandGrabInteractable>();

            //if (grabbable || handGrab || distanceGrab || distanceHandGrab)
            //{
            //    if (LeftHandGrabInteractor.IsGrabbing)
            //        LeftHandGrabInteractor.ForceRelease();
            //    if (RightHandGrabInteractor.IsGrabbing)
            //        RightHandGrabInteractor.ForceRelease();
            //    if (LeftHandControllerGrabInteractor.IsGrabbing)
            //        LeftHandControllerGrabInteractor.ForceRelease();
            //    if (RightHandControllerGrabInteractor.IsGrabbing)
            //        RightHandControllerGrabInteractor.ForceRelease();
            //    if (LeftDistanceHandGrabInteractor.IsGrabbing)
            //        LeftDistanceHandGrabInteractor.Unselect();
            //    if (RightDistanceHandGrabInteractor.IsGrabbing)
            //        RightDistanceHandGrabInteractor.Unselect();
            //    if (LeftDistanceHandControllerGrabInteractor.IsGrabbing)
            //        LeftDistanceHandControllerGrabInteractor.Unselect();
            //    if (RightDistanceHandControllerGrabInteractor.IsGrabbing)
            //        RightDistanceHandControllerGrabInteractor.Unselect();

            //    Debug.LogWarning("Hello!!");
            //    grabbable.Disable();
            //    handGrab.Disable();
            //    distanceGrab.Disable();
            //    distanceHandGrab.Disable();
            //}

            if (LeftHandGrabInteractor.IsGrabbing)
                LeftHandGrabInteractor.Unselect();
            if (RightHandGrabInteractor.IsGrabbing)
                RightHandGrabInteractor.Unselect();
            if (LeftHandControllerGrabInteractor.IsGrabbing)
                LeftHandControllerGrabInteractor.Unselect();
            if (RightHandControllerGrabInteractor.IsGrabbing)
                RightHandControllerGrabInteractor.Unselect();
            if (LeftDistanceHandGrabInteractor.IsGrabbing)
                LeftDistanceHandGrabInteractor.Unselect();
            if (RightDistanceHandGrabInteractor.IsGrabbing)
                RightDistanceHandGrabInteractor.Unselect();
            if (LeftDistanceHandControllerGrabInteractor.IsGrabbing)
                LeftDistanceHandControllerGrabInteractor.Unselect();
            if (RightDistanceHandControllerGrabInteractor.IsGrabbing)
                RightDistanceHandControllerGrabInteractor.Unselect();

            ExpectedObject.transform.position = ObjectFinalTransform.position;
            ExpectedObject.transform.rotation = ObjectFinalTransform.rotation;
            TargetPoint.SetActive(false);
            MarkedGhostObject.SetActive(false);

            OnEventTriggered?.Invoke();
        }
    }
}
