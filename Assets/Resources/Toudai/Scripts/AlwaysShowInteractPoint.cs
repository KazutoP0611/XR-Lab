using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysShowInteractPoint : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;

    private void Update()
    {
        _renderer.material.SetFloat("_Opacity", 1);
    }
}
