using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public GameObject SpinObject;
    public float SpinSpeed = 2;

    private void FixedUpdate()
    {
        float rotateToAngle = Time.deltaTime * SpinSpeed;
        if (rotateToAngle >= 360)
            rotateToAngle = 0;
        SpinObject.transform.Rotate(0, 0, rotateToAngle);
    }
}
