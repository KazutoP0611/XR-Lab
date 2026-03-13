using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using TMPro;

public enum Axis
{
    x,
    y,
    z
}

public class RotateObjectDetector : MonoBehaviour
{
    [Header("GameObject Settings")]
    public GameObject MarkedGhostObject;
    public bool ShowMarkedArrow = true;
    public GameObject MarkedArrow;
    public Transform ObjectFinalTransform;

    [Header("Axis Settings")]
    public Axis CheckByAxis;
    public float AngleDotThreshold = 0.99f;

    [Header("For Debugging")]
    public TextMeshProUGUI MarkedObjectText;
    public TextMeshProUGUI ExpectedObjectText;
    public TextMeshProUGUI DotProductObjectText;
    public TextMeshProUGUI OnInteractedText;

    private GameObject ExpectedObject;
    private Action OnEventTriggered;
    private bool CheckInteract = false;

    public void InitObjectDetector(GameObject SentExpectedObject, Action SentEventTriggered)
    {
        ExpectedObject = SentExpectedObject;
        OnEventTriggered = SentEventTriggered;

        MarkedArrow?.SetActive(ShowMarkedArrow);
        MarkedGhostObject.SetActive(true);
        CheckInteract = true;
    }

    public void FixedUpdate()
    {
        if (CheckInteract)
        {
            //Vector3 NormalizedExpectedObjectForward = ExpectedObject.transform.forward.normalized;
            //Vector3 NormalizedMarkGhostObject = MarkedGhostObject.transform.forward.normalized;

            Vector3 NormalizedExpectedObjectAxis = Vector3.zero;
            Vector3 NormalizedMarkGhostObjectAxis = Vector3.zero;

            switch (CheckByAxis)
            {
                case Axis.x:
                    NormalizedExpectedObjectAxis = ExpectedObject.transform.right.normalized;
                    NormalizedMarkGhostObjectAxis = MarkedGhostObject.transform.right.normalized;
                    break;
                case Axis.y:
                    NormalizedExpectedObjectAxis = ExpectedObject.transform.up.normalized;
                    NormalizedMarkGhostObjectAxis = MarkedGhostObject.transform.up.normalized;
                    break;
                case Axis.z:
                    NormalizedExpectedObjectAxis = ExpectedObject.transform.forward.normalized;
                    NormalizedMarkGhostObjectAxis = MarkedGhostObject.transform.forward.normalized;
                    break;
            }

            if (MarkedObjectText && ExpectedObjectText)
            {
                MarkedObjectText.text = NormalizedMarkGhostObjectAxis.ToString();
                ExpectedObjectText.text = NormalizedExpectedObjectAxis.ToString();
                DotProductObjectText.text = Vector3.Dot(NormalizedMarkGhostObjectAxis, NormalizedExpectedObjectAxis).ToString();
            }

            if (Vector3.Dot(NormalizedMarkGhostObjectAxis, NormalizedExpectedObjectAxis) >= AngleDotThreshold)
            {
                if (OnInteractedText)
                    OnInteractedText.text = "Passed";
                GrabInteractable grabbable = ExpectedObject.GetComponentInChildren<GrabInteractable>();
                HandGrabInteractable handGrab = ExpectedObject.GetComponentInChildren<HandGrabInteractable>();

                if (grabbable && handGrab)
                {
                    //Debug.LogWarning("Hello!!");
                    grabbable.enabled = false;
                    handGrab.enabled = false;
                }

                MarkedArrow?.SetActive(false);
                ExpectedObject.transform.rotation = ObjectFinalTransform.rotation;
                MarkedGhostObject.SetActive(false);

                OnEventTriggered?.Invoke();
            }
        }
    }
}
