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
    public string NormalTrigger;
    public string LeftToRightTrigger;
    public string RightToLeftTrigger;

    public void OpenPage(int page)
    {
        SetButton(page);

        if (page == 0)
        {
            GuideAnimator.SetTrigger(RightToLeftTrigger);
        }
        else
        {
            GuideAnimator.SetTrigger(LeftToRightTrigger);
        }

        //switch (page)
        //{
        //    case 0:
        //        //BeforeButtonToggle.interactable = false;
        //        //NextButtonToggle.interactable = true;
        //        //PreviousPageIndicator.color = SelectedIndicatorColor;
        //        //NextPageIndicator.color = UnselectedIndicatorColor;
        //        GuideAnimator.SetTrigger(RightToLeftTrigger);
        //        break;
        //    case 1:
        //        //BeforeButtonToggle.interactable = true;
        //        //NextButtonToggle.interactable = false;
        //        //PreviousPageIndicator.color = UnselectedIndicatorColor;
        //        //NextPageIndicator.color = SelectedIndicatorColor;
        //        GuideAnimator.SetTrigger(LeftToRightTrigger);
        //        break;
        //}
    }

    public void SetIndicator(int page)
    {
        SetButton(page);
        PreviousPageIndicator.color = page == 0 ? SelectedIndicatorColor : UnselectedIndicatorColor;
        NextPageIndicator.color = page == 0 ? UnselectedIndicatorColor : SelectedIndicatorColor;
    }

    private void SetButton(int page)
    {
        BeforeButtonToggle.interactable = page == 0 ? false : true;
        NextButtonToggle.interactable = page == 0 ? true : false;
    }

    public void SetToNormal()
    {
        GuideAnimator.SetTrigger(NormalTrigger);
    }

    //public void LeftToRight()
    //{
    //    GuideAnimator.SetTrigger(LeftToRightTrigger);
    //}

    //public void RightToLeft()
    //{
    //    GuideAnimator.SetTrigger(RightToLeftTrigger);
    //}

    public void TogglePanel(bool active)
    {
        PanelObject.SetActive(active);
        PanelGrabbingObject.SetActive(active);
    }
}
