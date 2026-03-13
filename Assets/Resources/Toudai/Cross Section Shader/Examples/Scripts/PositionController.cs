using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PositionController : MonoBehaviour
{
    [Header("Sliders")]
    public Slider posSlider;

    [Header("Transforms")]
    public Transform minTransform;
    public Transform maxTransform;

    [Header("Controll Object")]
    public GameObject controlObject;

    private void Start()
    {
        controlObject.transform.position = Vector3.Lerp(minTransform.position, maxTransform.position, posSlider.value);
    }

    public void UpdateObjectPosition()
    {
        controlObject.transform.position = Vector3.Lerp(minTransform.position, maxTransform.position, posSlider.value);
    }
}
