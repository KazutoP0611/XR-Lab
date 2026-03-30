using Oculus.Interaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

[Serializable]
public struct ModelMatData
{
    public Renderer rend;
    public Material originMat;
    public Material cutMat;
    public Material xrayMat;
}

public class ShowModel : MonoBehaviour
{
    private StateMachine stateMachine;
    private Dictionary<int, EntityState> animStates;
    private GameObject cutPanel;
    private bool cutShaderIsOn = false;

    private MainShowModelController showModelController;

    [SerializeField] private Animator anim;

    [Header("Model General Settings")]
    [SerializeField] private ModelMatData[] modelMatDatas;
    [SerializeField] private string shaderLocation;
    [SerializeField] private Collider modelInteractCollider;

    public void InitModel(MainShowModelController showModelController)
    {
        stateMachine = new StateMachine();
        this.showModelController = showModelController;

        // Assign cut panel
        cutPanel = showModelController.cutPanelObject;

        // Assign animation data
        var animationDataDict = showModelController.animationDataDict;
        animStates = new Dictionary<int, EntityState>();

        for (int i = 0; i < animationDataDict.Count; i++)
        {
            EntityState state = new EntityState(anim, animationDataDict[i].animParam);
            animStates.Add(i, state);
        }

        stateMachine.Initialize(animStates[showModelController.GetCurrentAnimationIndex()]);
    }

    private void Update()
    {
        if (cutShaderIsOn)
            UpdateCutPanelPositionInShader();
    }

    private void UpdateCutPanelPositionInShader()
    {
        foreach (var modelData in modelMatDatas)
        {
            if (modelData.rend.material.shader.name == shaderLocation)
            {
                modelData.rend.material.SetVector("_PanelPos", cutPanel.transform.position);
                modelData.rend.material.SetVector("_PanelNormal", cutPanel.transform.TransformVector(new Vector3(0, 1, 0)));
            }
        }
    }

    public void ChangeAnimation(int currentAnimationIndex)
    {
        EntityState changeToState = animStates[currentAnimationIndex];
        stateMachine.ChangeState(changeToState);
    }

    public void SetAnimationSpeed(bool isPlaying)
    {
        // This will change the entire animator's speed, so all animations will be affected by this change.
        //anim.speed = isPlaying ? 1 : 0;

        // This will also change anim speed, but it will only affect the current animation that is playing.
        // So if the animation is paused, it will not affect the speed of the animation when it is played again.
        anim.SetFloat("AnimSpeed", isPlaying ? 1 : 0);
    }

    public virtual void SetModelMaterialFromMode(ModelMatMode modelMode)
    {
        switch (modelMode)
        {
            case ModelMatMode.NormalMat:
                foreach (var modelData in modelMatDatas)
                    modelData.rend.material = modelData.originMat;
                break;
            case ModelMatMode.CutMat:
                foreach (var modelData in modelMatDatas)
                    modelData.rend.material = modelData.cutMat;
                break;
            case ModelMatMode.XrayMat:
                foreach (var modelData in modelMatDatas)
                    modelData.rend.material = modelData.xrayMat;
                break;
        }
    }

    public void SetMovableComponents(bool enable) =>  modelInteractCollider.enabled = enable;

    public void ActivateCutShader(bool activate) => cutShaderIsOn = activate;
}
