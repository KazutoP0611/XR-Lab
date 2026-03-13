using UnityEngine;

[CreateAssetMenu(fileName = "ModelData_", menuName = "Scriptable Objects/ModelData_SO")]
public class ModelData_SO : ScriptableObject
{
    [SerializeField] private AnimationClip[] animations;
}
