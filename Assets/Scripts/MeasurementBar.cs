using System;
using TMPro;
using UnityEngine;

public class MeasurementBar : MonoBehaviour
{
    private float originalBarSize;

    [Header("General Details")]
    [SerializeField] private Transform barParent;
    [SerializeField] private GameObject barObject;
    [SerializeField] private float divideForSize = 2.0f;

    [Header("Grab Points")]
    [SerializeField] private Transform firstTransform;
    [SerializeField] private Transform secondTransform;

    [Header("Text Details")]
    [SerializeField] private TextMeshProUGUI lengthText;

    private void Start()
    {
        originalBarSize = barObject.transform.localScale.y;
    }

    private void Update()
    {
        AdjustingBarObject();
    }

    private void AdjustingBarObject()
    {
        // Rotate Parent Object, pointing to both grab points;
        Vector3 pointingVector = secondTransform.position - firstTransform.position;
        barParent.right = pointingVector;

        // Change scale of object according to distance of 2 grab points;
        float size = Vector3.Distance(secondTransform.position, firstTransform.position);
        barObject.transform.localScale = new Vector3(originalBarSize, size / divideForSize, originalBarSize);

        // Update text
        UpdateText(size);

        // Set parent of bar object between 2 grab points;
        barParent.position = firstTransform.position + (pointingVector.normalized * (size / 2));
    }

    private void UpdateText(float length)
    {
        string measurement = length >= 1 ? "m" : "cm";
        float newLength = length >= 1 ? length : (int)(length * 100);
        lengthText.text = newLength.ToString("#.##") + $" {measurement}";
    }
}
