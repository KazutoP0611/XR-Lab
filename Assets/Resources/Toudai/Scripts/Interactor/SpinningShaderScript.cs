using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningShaderScript : MonoBehaviour
{
    public Renderer ObjectRenderer;
    public float Speed = 2.0f;
    public bool OppositeYaw = false;

    private Material ObjectMaterial;
    float OffSetValue = 0;

    private void Start()
    {
        ObjectMaterial = ObjectRenderer.materials[0];
    }

    private void FixedUpdate()
    {
        OffSetValue += Time.deltaTime * Speed * (OppositeYaw ? -1 : 1);
        ObjectMaterial.SetTextureOffset("_BaseMap", new Vector2(OffSetValue, 0));
        //Debug.LogWarning($"Value: {OffSetValue}");
        //ObjectMaterial.SetTextureOffset(Shader.PropertyToID("_Offset"), new Vector2(OffSetValue, 0));
    }
}
