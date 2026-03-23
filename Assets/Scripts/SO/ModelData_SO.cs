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

    [Header("Real Size Settings")]
    public Vector3 spawnPosition;
    public Vector3 spawnAngle;
    public Vector3 spawnScale;

    [Header("Hand Size Settings")]
    public bool handSize = false;
    public Vector3 handSizePositionOffset;
    public Vector3 handSizeAngle;
    public Vector3 HandSizeScale;
}
