using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public delegate void AnimationEndEvent();
    public AnimationEndEvent OnOpenEndEvent;
    public AnimationEndEvent OnCloseEndEvent;

    public GameObject PanelGameObject;
    public Animator PanelAnimator;

    public virtual void OpenPanel()
    {
        //PanelGameObject.SetActive(true);
        PanelAnimator.SetTrigger("Open");
    }

    public void SetOnOpenAnimationEndEvent(AnimationEndEvent SentOnOpenEndEvent = null)
    {
        OnOpenEndEvent = SentOnOpenEndEvent;
    }

    public virtual void ClosePanel()
    {
        PanelAnimator.SetTrigger("Close");
    }

    public void SetOnCloseAnimationEndEvent(AnimationEndEvent SentOnClostEndEvent = null)
    {
        OnCloseEndEvent = SentOnClostEndEvent;
    }

    public virtual void OnOpenAnimationEnd()
    {
        OnOpenEndEvent?.Invoke();
        OnOpenEndEvent = null;
    }

    public virtual void OnCloseAnimationEnd()
    {
        //PanelAnimator.SetTrigger("Idle");
        //PanelGameObject.SetActive(false);
        OnCloseEndEvent?.Invoke();
        OnCloseEndEvent = null;
    }

    //public virtual void ClearDelegates()
    //{
    //    OnOpenEndEvent = null;
    //    OnCloseEndEvent = null;
    //}
}
