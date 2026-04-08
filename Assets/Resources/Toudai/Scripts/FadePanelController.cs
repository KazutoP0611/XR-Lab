using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class FadePanelController : MonoBehaviour
{
    public Image FadePanel;
    public Color NormalColor;
    public Color BlackColor;
    public float FadeInSecs = 2.0f;

    public static FadePanelController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FadeToNormal();
    }

    public void FadeToBlack(Action EndOfFade = null)
    {
        FadePanel.DOColor(BlackColor, FadeInSecs)
            .OnComplete(() => {
                EndOfFade?.Invoke();
            });
    }

    public void FadeToNormal(Action EndOfFade = null)
    {
        FadePanel.DOColor(NormalColor, FadeInSecs)
            .OnComplete(() => {
                EndOfFade?.Invoke();
            });
    }
}
