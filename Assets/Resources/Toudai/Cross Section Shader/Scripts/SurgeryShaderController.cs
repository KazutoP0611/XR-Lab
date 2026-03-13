using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurgeryShaderController : MonoBehaviour
{
    public Renderer[] rends;
    public GameObject plane;
    public string ShaderLocation;

    // Start is called before the first frame update
    void Start()
    {
        if (rends.Length == 0)
        {
            Debug.LogWarning($"There is no ref to renderer component, system will GetComponent automatically.");
            //rend = GetComponent<Renderer>();
        }
        else
        {
            UpdateShaderProperties();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateShaderProperties();
    }

    private void UpdateShaderProperties()
    {
        for (int i = 0; i < rends.Length; i++)
        {
            if (rends[i].material.shader.name == ShaderLocation)
            {
                rends[i].material.SetVector("_PanelPos", plane.transform.position);
                rends[i].material.SetVector("_PanelNormal", plane.transform.TransformVector(new Vector3(0, 1, 0)));
            }
        }
    }

    public void SetModelMaterial(Material SentMaterial)
    {
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material = SentMaterial;
        }
    }

    //This is a fucking special way to change material, do not use this code in any other way.
    public void SetModelXRayMode(Material NormalMateril, Material xrayBlueMaterial, Material xrayRedMaterial, Material xrayYellowMaterial, Material xrayPurpleMaterial, Material xrayGreenMaterial)
    {
        rends[0].material = xrayRedMaterial;
        rends[1].material = xrayBlueMaterial;
        rends[2].material = xrayBlueMaterial;
        rends[3].material = xrayBlueMaterial;
        rends[4].material = xrayRedMaterial;
    }
}
