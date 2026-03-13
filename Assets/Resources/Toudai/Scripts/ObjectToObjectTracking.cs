using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToObjectTracking : MonoBehaviour
{
    public Transform TrackingObject;
    public float DotThreshold = 0.0f;
    public GameObject ShowObject;

    private void Start()
    {
        ShowObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 ThisObjectToTrackObjectVector = (TrackingObject.position - transform.forward);
        Vector3 TrackObjectToThisObjectVector = (transform.position - TrackingObject.position);

        if (Vector3.Dot(ThisObjectToTrackObjectVector, TrackObjectToThisObjectVector) > DotThreshold)
        {
            ShowObject.SetActive(true);
        }
        else
        {
            ShowObject.SetActive(false);
        }
    }
}
