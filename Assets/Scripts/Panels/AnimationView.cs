using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnimationView : MonoBehaviour
{
    private MainShowModelController animController;
    private AnimationData[] animationDatas;
    
    private Action OnPlayTitleEndAction;

    [Header("Texts")]
    public TextMeshProUGUI FarLeftTitle;
    public TextMeshProUGUI CloseLeftTitle;
    public TextMeshProUGUI CenterTitle;
    public TextMeshProUGUI CloseRightTitle;
    public TextMeshProUGUI FarRightTitle;

    [Header("Animator")]
    public Animator TitleAnimator;

    [Header("Play Button Settings")]
    public Image PlayButtonIcon;
    [Space]
    public Sprite PlaySprite;
    public Sprite PauseSprite;

    [Header("Mode Button Settings")]
    [SerializeField] private Toggle freeModeButton;
    [SerializeField] private Toggle cutModeButton;
    [SerializeField] private Toggle giantSizeButton;

    public void InitTitleView(MainShowModelController animController)
    {
        this.animController = animController;
        animationDatas = animController.animationDatas;

        UpdateAnimationTitles();

        // Set up buttons
        EnableGiantSizeButton(animController.modeldata.giantSize);
    }

    public void PlayTitleAnimation(PanelAnimation animToPlay, Action OnPlayTitleEnd = null)
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

        int currentAnimIndex = animController.GetCurrentAnimationIndex();
        CenterTitle.text = animationDatas[currentAnimIndex].animationTitle;

        #region Left Titles
        // If the current animation index is the FIRST ONE or the SECOND ONE of the list, set the close left and far left titles to the last two titles in the list
        if (currentAnimIndex - 1 <= 0)
        {
            // If the current animation index is the FIRST ONE of the list
            if (currentAnimIndex == 0)
            {
                CloseLeftTitle.text = animationDatas[animationDatas.Count() - 1].animationTitle; // Set the close left title to the last animation title
                FarLeftTitle.text = animationDatas[animationDatas.Count() - 2].animationTitle; // Set the far left title to the second to last animation title
            }
            // If the current animation index is the SECOND ONE of the list
            else if (currentAnimIndex - 1 == 0)
            {
                CloseLeftTitle.text = animationDatas[0].animationTitle; // Set the close left title to the first animation title
                FarLeftTitle.text = animationDatas[animationDatas.Count() - 1].animationTitle; // Set the far left title to the last animation title
            }
        }
        // If the current animation index is not the FIRST ONE or the SECOND ONE of the list, set the close left and far left titles to the previous two titles in the list
        else
        {
            CloseLeftTitle.text = animationDatas[currentAnimIndex - 1].animationTitle;
            FarLeftTitle.text = animationDatas[currentAnimIndex - 2].animationTitle;
        }
        #endregion

        #region Right Titles
        // If the current animation index is the LAST ONE or the SECOND TO LAST ONE of the list, set the close right and far right titles to the first two titles in the list
        if (currentAnimIndex + 1 >= animationDatas.Count() - 1)
        {
            // If the current animation index is the LAST ONE of the list
            if (currentAnimIndex == animationDatas.Count() - 1)
            {
                CloseRightTitle.text = animationDatas[0].animationTitle; // Set the close right title to the first animation title
                FarRightTitle.text = animationDatas[1].animationTitle; // Set the far right title to the second animation title
            }
            // If the current animation index is the SECOND TO LAST ONE of the list
            else if (currentAnimIndex + 1 == animationDatas.Count() - 1)
            {
                CloseRightTitle.text = animationDatas[animationDatas.Count() - 1].animationTitle; // Set the close right title to the last animation title
                FarRightTitle.text = animationDatas[0].animationTitle; // Set the far right title to the first animation title
            }
        }
        // If the current animation index is not the LAST ONE or the SECOND TO LAST ONE of the list, set the close right and far right titles to the next two titles in the list
        else
        {
            CloseRightTitle.text = animationDatas[currentAnimIndex + 1].animationTitle;
            FarRightTitle.text = animationDatas[currentAnimIndex + 2].animationTitle;
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

    public void EnableFreeModeButton(bool enable) => freeModeButton.interactable = enable;

    public void EnablePanelObjectButton(bool enable) => cutModeButton.interactable = enable;

    public void EnableGiantSizeButton(bool enable) => giantSizeButton.interactable = enable;
}
