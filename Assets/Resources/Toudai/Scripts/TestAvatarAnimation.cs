using EzySlice;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestAvatarAnimation : MonoBehaviour
{
    [Header("Animator")]
    public Animator Animator;

    [Header("Animation Controllers")]
    public Slider Slider;
    public TextMeshProUGUI Text;

    private bool IsAnimPlaying = false;
    private float time = 0;

    public void ToggleAnimation()
    {
        if (IsAnimPlaying)
        {
            Animator.SetBool("Verti", false);
            IsAnimPlaying = false;
        }
        else
        {
            Animator.SetBool("Verti", true);
            IsAnimPlaying = true;
        }
    }

    private void Update()
    {
        if (IsAnimPlaying)
        {
            time += Time.deltaTime;
            Slider.value = time;
            SetAnim();
        }
    }

    public void ResetAnimtion()
    {
        time = 0;
        Slider.value = 0;
        Animator.SetFloat("Time", 0);
    }

    public void SetAnimTime()
    {
        time = Slider.value;
        SetAnim();
    }

    private void SetAnim()
    {
        float AnimTime = Mathf.InverseLerp(Slider.minValue, Slider.maxValue, time);
        Text.text = AnimTime.ToString();
        Animator.SetFloat("Time", AnimTime);
    }
}
