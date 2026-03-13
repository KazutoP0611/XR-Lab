using UnityEngine;
using System.Collections;

public class ThreeAAPlanesCuttingController : MonoBehaviour {

    public GameObject planeYZ;
    public GameObject planeXZ;
    public GameObject planeXY;

    Material mat;

    public Vector3 positionYZ;
    public Vector3 positionXZ;
    public Vector3 positionXY;

    public Renderer rend;

    // Use this for initialization
    void Start()
    {
        if (!rend)
        {
            Debug.LogWarning($"There is no ref to renderer component, system will GetComponent automatically.");
            rend = GetComponent<Renderer>();
        }
        
        UpdateShaderProperties();
    }
    void Update()
    {
        UpdateShaderProperties();
    }

    private void UpdateShaderProperties()
    {
        positionYZ = planeYZ.transform.localPosition;
        positionXZ = planeXZ.transform.localPosition;
        positionXY = planeXY.transform.localPosition;

        for (int i = 0; i < rend.materials.Length; i++)
        {
            if (rend.materials[i].shader.name == "Shader Graphs/ThreeDimensionObject")
            {
                rend.materials[i].SetVector("_PlaneXPos", positionYZ);
                rend.materials[i].SetVector("_PlaneYPos", positionXZ);
                rend.materials[i].SetVector("_PlaneZPos", positionXY);
            }
        }
      
    }
}
