using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PPLookAt : MonoBehaviour
{
    [SerializeField]
    private Transform _toRotate;

    [SerializeField]
    private Transform _target;

    private void Start()
    {
        this.AssertField(_toRotate, nameof(_toRotate));
        this.AssertField(_target, nameof(_target));
    }

    protected virtual void Update()
    {
        Vector3 LookAtTarget = new Vector3(_target.position.x, _toRotate.position.y, _target.position.z);
        Vector3 ToTargetDir = (LookAtTarget - _toRotate.position).normalized;
        _toRotate.LookAt(_toRotate.position - ToTargetDir, Vector3.up);
    }
}
