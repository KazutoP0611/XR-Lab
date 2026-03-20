using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct XrayModeData
{
    public Renderer rend;
    public Material originMat;
    public Material xrayMat;
}

public class ShowModel : MonoBehaviour
{
    private StateMachine stateMachine;

    private Dictionary<int, EntityState> animStates;

    [SerializeField] private Animator anim;

    [Header("X-Ray Mode Data Settings")]
    [SerializeField] private XrayModeData[] xrayModeDatas;

    private void Start()
    {
        
    }

    public void InitModel(Dictionary<int, AnimationData> animationDatas, int currentAnimationIndex)
    {
        stateMachine = new StateMachine();

        animStates = new Dictionary<int, EntityState>();
        for (int i = 0; i < animationDatas.Count; i++)
        {
            EntityState state = new EntityState(anim, animationDatas[i].animParam);
            animStates.Add(i, state);
        }

        stateMachine.Initialize(animStates[currentAnimationIndex]);
    }

    //private void Update()
    //{
    //    stateMachine.UpdateActiveState();
    //}

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

    public virtual void SetModelXrayMode(bool xrayMode)
    {
        foreach (var xrayData in xrayModeDatas)
        {
            xrayData.rend.material = xrayMode ? xrayData.xrayMat : xrayData.originMat;
        }
    }
}
