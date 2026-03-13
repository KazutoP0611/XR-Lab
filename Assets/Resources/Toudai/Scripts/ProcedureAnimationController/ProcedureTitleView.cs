using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProcedureTitleView : MonoBehaviour
{
    public delegate int GetCurrentAnimationIndex();
    private GetCurrentAnimationIndex GetCurrentAnimIndex;

    public enum AnimationToPlay
    {
        Left,
        Right
    }

    [Header("Texts")]
    public TextMeshProUGUI FarLeftTitle;
    public TextMeshProUGUI CloseLeftTitle;
    public TextMeshProUGUI CenterTitle;
    public TextMeshProUGUI ClostRightTitle;
    public TextMeshProUGUI FarRightTitle;

    [Header("Animation Titles")]
    public string[] AnimationTitles;

    [Header("Animator")]
    public Animator TitleAnimator;

    [Header("Play Button Settings")]
    public Image PlayButtonIcon;
    public Sprite PlaySprite;
    public Sprite PauseSprite;

    private Action OnPlayTitleEndAction;

    public void InitTitleView(GetCurrentAnimationIndex SentGetCurrentAnimIndexMethod)
    {
        GetCurrentAnimIndex = SentGetCurrentAnimIndexMethod;
        UpdateAnimationTitles();
    }

    public void PlayTitleAnimation(AnimationToPlay animToPlay, Action OnPlayTitleEnd = null)
    {
        OnPlayTitleEndAction = OnPlayTitleEnd;
        TitleAnimator.SetTrigger(animToPlay.ToString());
    }

    public void OnAnimationEnd()
    {
        UpdateAnimationTitles();
    }

    private void UpdateAnimationTitles()
    {
        OnPlayTitleEndAction?.Invoke();

        int currentAnimIndex = GetCurrentAnimIndex();
        CenterTitle.text = AnimationTitles[currentAnimIndex];

        if (currentAnimIndex - 1 <= 0)
        {
            if (currentAnimIndex == 0)
            {
                CloseLeftTitle.text = AnimationTitles[AnimationTitles.Count() - 1];
                FarLeftTitle.text = AnimationTitles[AnimationTitles.Count() - 2];
            }
            else if (currentAnimIndex - 1 == 0)
            {
                CloseLeftTitle.text = AnimationTitles[0];
                FarLeftTitle.text = AnimationTitles[AnimationTitles.Count() - 1];
            }
        }
        else
        {
            CloseLeftTitle.text = AnimationTitles[currentAnimIndex - 1];
            FarLeftTitle.text = AnimationTitles[currentAnimIndex - 2];
        }

        if (currentAnimIndex + 1 >= AnimationTitles.Count() - 1)
        {
            if (currentAnimIndex == AnimationTitles.Count() - 1)
            {
                ClostRightTitle.text = AnimationTitles[0];
                FarRightTitle.text = AnimationTitles[1];
            }
            else if (currentAnimIndex + 1 == AnimationTitles.Count() - 1)
            {
                ClostRightTitle.text = AnimationTitles[AnimationTitles.Count() - 1];
                FarRightTitle.text = AnimationTitles[0];
            }
        }
        else
        {
            ClostRightTitle.text = AnimationTitles[currentAnimIndex + 1];
            FarRightTitle.text = AnimationTitles[currentAnimIndex + 2];
        }
    }

    public void SetPlayButton(bool isPlaying)
    {
        if (isPlaying)
            PlayButtonIcon.sprite = PauseSprite;
        else
            PlayButtonIcon.sprite = PlaySprite;
    }
}
