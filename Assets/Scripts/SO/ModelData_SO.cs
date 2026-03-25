using System;
using UnityEngine;

[Serializable]
public struct AnimationData
{
    public string animParam;
    public string animationTitle;
}

[CreateAssetMenu(fileName = "ModelData_", menuName = "Scriptable Objects/ModelData_SO")]
public class ModelData_SO : ScriptableObject
{
    public GameObject modelPrefab; // prefab that has "ShowModel" component;
    public AnimationData[] animationDatas;

    [Header("Giant Size Settings")]
    public bool giantSize = false;
    public Vector3 giantSizePosition;
    public Vector3 giantSizeRotate;
    public Vector3 giantSizeScale;

    [Header("Small Size Settings")]
    public Vector3 smallSizePositionOffset;
    public Vector3 smallSizeRotate;
    public Vector3 smallSizeScale;
}
