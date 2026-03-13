using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Panel IntructionPanel;
    public float WaitForSecsBeforeClosePanel;

    protected Action OnInteractEndEvent;
    protected bool interacted = false;

    public virtual void Init(Action SentOnInteractEvent)
    {
        OnInteractEndEvent = SentOnInteractEvent;

        IntructionPanel.OpenPanel();
    }

    public virtual void OnInteracted()
    {
        if (!interacted)
        {
            interacted = true;
            IntructionPanel.SetOnCloseAnimationEndEvent(() =>
            {
                OnInteractEndEvent?.Invoke();
            });

            StartCoroutine(WaitForSeconds(WaitForSecsBeforeClosePanel));

            IntructionPanel.ClosePanel();
        }
    }

    protected IEnumerator WaitForSeconds(float secs, Action FinishedWaitEvent = null)
    {
        yield return new WaitForSeconds(secs);

        FinishedWaitEvent?.Invoke();
    }
}
