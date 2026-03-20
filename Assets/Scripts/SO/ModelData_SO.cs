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
    public Vector3 spawnPosition;
    public Vector3 spawnAngle;
    public bool handSize = false;
}
