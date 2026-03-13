using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationView : MonoBehaviour
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
    public TextMeshProUGUI CloseRightTitle;
    public TextMeshProUGUI FarRightTitle;

    [Header("Animation Titles")]
    public string[] AnimationTitles;

    [Header("Animator")]
    public Animator TitleAnimator;

    [Header("Play Button Settings")]
    public Image PlayButtonIcon;
    [Space]
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

        #region Left Titles
        // If the current animation index is the FIRST ONE or the SECOND ONE of the list, set the close left and far left titles to the last two titles in the list
        if (currentAnimIndex - 1 <= 0)
        {
            // If the current animation index is the FIRST ONE of the list
            if (currentAnimIndex == 0)
            {
                CloseLeftTitle.text = AnimationTitles[AnimationTitles.Count() - 1]; // Set the close left title to the last animation title
                FarLeftTitle.text = AnimationTitles[AnimationTitles.Count() - 2]; // Set the far left title to the second to last animation title
            }
            // If the current animation index is the SECOND ONE of the list
            else if (currentAnimIndex - 1 == 0)
            {
                CloseLeftTitle.text = AnimationTitles[0]; // Set the close left title to the first animation title
                FarLeftTitle.text = AnimationTitles[AnimationTitles.Count() - 1]; // Set the far left title to the last animation title
            }
        }
        // If the current animation index is not the FIRST ONE or the SECOND ONE of the list, set the close left and far left titles to the previous two titles in the list
        else
        {
            CloseLeftTitle.text = AnimationTitles[currentAnimIndex - 1];
            FarLeftTitle.text = AnimationTitles[currentAnimIndex - 2];
        }
        #endregion

        #region Right Titles
        // If the current animation index is the LAST ONE or the SECOND TO LAST ONE of the list, set the close right and far right titles to the first two titles in the list
        if (currentAnimIndex + 1 >= AnimationTitles.Count() - 1)
        {
            // If the current animation index is the LAST ONE of the list
            if (currentAnimIndex == AnimationTitles.Count() - 1)
            {
                CloseRightTitle.text = AnimationTitles[0]; // Set the close right title to the first animation title
                FarRightTitle.text = AnimationTitles[1]; // Set the far right title to the second animation title
            }
            // If the current animation index is the SECOND TO LAST ONE of the list
            else if (currentAnimIndex + 1 == AnimationTitles.Count() - 1)
            {
                CloseRightTitle.text = AnimationTitles[AnimationTitles.Count() - 1]; // Set the close right title to the last animation title
                FarRightTitle.text = AnimationTitles[0]; // Set the far right title to the first animation title
            }
        }
        // If the current animation index is not the LAST ONE or the SECOND TO LAST ONE of the list, set the close right and far right titles to the next two titles in the list
        else
        {
            CloseRightTitle.text = AnimationTitles[currentAnimIndex + 1];
            FarRightTitle.text = AnimationTitles[currentAnimIndex + 2];
        }
        #endregion
    }

    public void SetPlayButton(bool isPlaying)
    {
        if (isPlaying)
            PlayButtonIcon.sprite = PauseSprite;
        else
            PlayButtonIcon.sprite = PlaySprite;
    }
}
