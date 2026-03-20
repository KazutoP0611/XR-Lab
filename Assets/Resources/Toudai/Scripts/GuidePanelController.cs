using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GuidePanelController : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject PanelObject;
    public GameObject PanelGrabbingObject;

    [Header("Toggle Components")]
    public Toggle BeforeButtonToggle;
    public Toggle NextButtonToggle;

    [Header("Page Indicator Components")]
    public Image PreviousPageIndicator;
    public Image NextPageIndicator;
    public Color SelectedIndicatorColor;
    public Color UnselectedIndicatorColor;

    [Header("Animations")]
    public Animator GuideAnimator;

    public void OpenPage(int page)
    {
        if (page == 0)
            GuideAnimator.SetTrigger(PanelAnimation.Left.ToString());
        else
            GuideAnimator.SetTrigger(PanelAnimation.Right.ToString());
    }

    public void SetGuidePanelPageIndicator(int page)
    {
        SetLeftAndRightButtons(page);
        PreviousPageIndicator.color = page == 0 ? SelectedIndicatorColor : UnselectedIndicatorColor;
        NextPageIndicator.color = page == 0 ? UnselectedIndicatorColor : SelectedIndicatorColor;
    }

    private void SetLeftAndRightButtons(int page)
    {
        BeforeButtonToggle.interactable = page == 0 ? false : true;
        NextButtonToggle.interactable = page == 0 ? true : false;
    }

    //public void SetToNormal()
    //{
    //    GuideAnimator.SetTrigger(PanelAnimation.Normal.ToString());
    //}

    public void TogglePanel(bool active)
    {
        PanelObject.SetActive(active);
        PanelGrabbingObject.SetActive(active);
    }
}
