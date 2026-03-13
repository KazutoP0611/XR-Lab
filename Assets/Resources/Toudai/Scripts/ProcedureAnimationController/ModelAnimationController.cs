using Meta.WitAi.Speech;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelAnimationController : MonoBehaviour
{
    [Header("Panel Animation Settings")]
    public ProcedureTitleView ProcedureTitleViewController;

    [Header("Head Animation Settings")]
    public Animator HeadAnimator;
    public AnimationClip[] HeadMannequinAnimations;
    public GameObject TubeObject;

    [Header("Slider")]
    public Slider AnimationTimeline;
    public TextMeshProUGUI Text;

    private int currentAnimationIndex;
    private float time;
    private bool animIsPlaying = false;

    private void Start()
    {
        ProcedureTitleViewController.InitTitleView(GetCurrentAnimation);
        currentAnimationIndex = 0;
        AnimationTimeline.value = 0;
        time = 0;
        HeadAnimator.Play(HeadMannequinAnimations[currentAnimationIndex].name, -1, time);
        //CheckToOpenTube();
    }

    private void Update()
    {
        if (animIsPlaying)
        {
            if (Mathf.InverseLerp(0, HeadMannequinAnimations[currentAnimationIndex].length, time) < 1)
            {
                time += Time.deltaTime;
                SetAnim();
            }
            else
            {
                time = 0;
                PauseAnimation();
            }
        }
    }

    public void GotoLeftAnimation()
    {
        ++currentAnimationIndex;

        ProcedureTitleViewController.PlayTitleAnimation(ProcedureTitleView.AnimationToPlay.Left, CheckAnimationCount);
        //CheckAnimationCount();
    }
     
    public void GotoRightAnimation()
    {
        --currentAnimationIndex;

        ProcedureTitleViewController.PlayTitleAnimation(ProcedureTitleView.AnimationToPlay.Right, CheckAnimationCount);
        //CheckAnimationCount();
    }

    public void PlayHeadAnimation()
    {
        if (!animIsPlaying && (time >= 0))
        {
            PlayAnimation();
        }
        else
        {
            PauseAnimation();
        }
    }

    private void PlayAnimation()
    {
        animIsPlaying = true;
        HeadAnimator.Play(HeadMannequinAnimations[currentAnimationIndex].name, -1, time);
        HeadAnimator.SetFloat("Speed", 1);
        ProcedureTitleViewController.SetPlayButton(true);
    }

    private void PauseAnimation()
    {
        animIsPlaying = false;
        HeadAnimator.SetFloat("Speed", 0);
        ProcedureTitleViewController.SetPlayButton(false);
    }

    public void ReplayAnimation()
    {
        if (time > 0)
        {
            time = 0;
            PlayAnimation();
        }
    }

    public void PlayAnimationAtTime()
    {
        time = Mathf.Lerp(0, HeadMannequinAnimations[currentAnimationIndex].length, AnimationTimeline.value);
        float AnimTime = Mathf.InverseLerp(0, HeadMannequinAnimations[currentAnimationIndex].length, time);
        HeadAnimator.SetFloat("Time", AnimTime);
    }

    private void CheckAnimationCount()
    {
        PauseAnimation();

        //time = 0;
        //AnimationTimeline.value = 0;

        //if (currentAnimationIndex >= 0 && currentAnimationIndex < HeadMannequinAnimations.Count())
        //{
        //    //HeadAnimator.Play(HeadMannequinAnimations[currentAnimationIndex].name, -1, time);
        //}
        if (currentAnimationIndex < 0)
        {
            currentAnimationIndex = (HeadMannequinAnimations.Length - 1);
            //HeadAnimator.Play(HeadMannequinAnimations[currentAnimationIndex].name, -1, time);
        }
        else if (currentAnimationIndex >= HeadMannequinAnimations.Length)
        {
            currentAnimationIndex = 0;
            //HeadAnimator.Play(HeadMannequinAnimations[currentAnimationIndex].name, -1, time);
        }

        time = 0;
        AnimationTimeline.value = 0;
        //Text.text = currentAnimationIndex.ToString();
        //CheckToOpenTube()
        HeadAnimator.Play(HeadMannequinAnimations[currentAnimationIndex].name, -1, time);
    }

    private void CheckToOpenTube()
    {
        TubeObject.SetActive(currentAnimationIndex == HeadMannequinAnimations.Length - 1);
    }

    public int GetCurrentAnimation()
    {
        return currentAnimationIndex;
    }

    private void SetAnim()
    {
        float AnimTime = Mathf.InverseLerp(0, HeadMannequinAnimations[currentAnimationIndex].length, time);

        //Text.text = AnimTime.ToString();
        HeadAnimator.SetFloat("Time", AnimTime);
        AnimationTimeline.value = AnimTime;
    }
}
