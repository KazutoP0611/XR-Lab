using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Oculus.Interaction;

public class MannequinModeController : MonoBehaviour
{
    [Serializable]
    public enum MannequinMode
    {
        Normal = 0,
        VerticalCut = 1,
        HorizontalCut = 2,
        SurgeryMode = 3,
        XRay = 4
    }

    [Header("Controller")]
    public SurgeryShaderController SurgeryShaderController;

    [Header("General Settings")]
    public Transform MannequinMainTransform;
    public Transform _headAnchor;

    [Header("Mannequin Reposition Settings")]
    public GameObject MannequinHead;
    public Vector3 HeadSpawnOffset;

    [Header("Surgery Panel Settings")]
    public GameObject SurgeryPanel;
    public Vector3 SpawnOffset;
    public bool ActiveSurgeryPanelWhenInOtherMode = true;

    [Header("Verticle Mode")]
    public Transform VerticleCutPanelTransform;

    [Header("Horizontal Mode")]
    public Transform HorizontalCutPanelTransform;

    [Header("Materials")]
    public Material NormalMat;
    public Material SurgeryMat;

    [Header("XRay Materials")]
    public Material xrayBlueMat;
    public Material xrayRedMat;
    public Material xrayYellowMat;
    public Material xrayPurpleMat;
    public Material xrayGreenMat;

    private MannequinMode CurrentMode = MannequinMode.Normal;

    private void Start()
    {
        SelectMode(0);
    }

    public void SelectMode(int selectedMode)
    {
        if (selectedMode == (int)CurrentMode)
        {
            CurrentMode = MannequinMode.Normal;
            ToggleMode(0);
            return;
        }

        ToggleMode(selectedMode);
    }    

    private void ToggleMode(int selectedMode)
    {
        switch (selectedMode)
        {
            case 0:
                CurrentMode = MannequinMode.Normal;

                //rend.material = NormalMat;
                SurgeryShaderController.SetModelMaterial(NormalMat);
                SurgeryPanel.SetActive(ActiveSurgeryPanelWhenInOtherMode);
                //SurgeryPanel.transform.SetParent(MannequinMainTransform);
                SetPanelSpawnPosition();

                break;
            case 1:
                CurrentMode = MannequinMode.VerticalCut;

                //rend.material = SurgeryMat;
                SurgeryShaderController.SetModelMaterial(SurgeryMat);
                SurgeryPanel.SetActive(ActiveSurgeryPanelWhenInOtherMode);
                SurgeryPanel.transform.position = VerticleCutPanelTransform.position;
                SurgeryPanel.transform.rotation = VerticleCutPanelTransform.rotation;
                SurgeryPanel.transform.parent = VerticleCutPanelTransform;

                break;
            case 2:
                CurrentMode = MannequinMode.HorizontalCut;

                //rend.material = SurgeryMat;
                SurgeryShaderController.SetModelMaterial(SurgeryMat);
                SurgeryPanel.SetActive(ActiveSurgeryPanelWhenInOtherMode);
                SurgeryPanel.transform.position = HorizontalCutPanelTransform.position;
                SurgeryPanel.transform.rotation = HorizontalCutPanelTransform.rotation;
                SurgeryPanel.transform.parent = HorizontalCutPanelTransform;

                break;
            case 3:
                CurrentMode = MannequinMode.SurgeryMode;

                //rend.material = SurgeryMat;
                SurgeryShaderController.SetModelMaterial(SurgeryMat);
                SurgeryPanel.SetActive(true);
                //SurgeryPanel.transform.SetParent(MannequinMainTransform);
                SetPanelSpawnPosition();

                break;
            case 4:
                CurrentMode = MannequinMode.XRay;

                //rend.material = XRayMat;
                SurgeryShaderController.SetModelXRayMode(NormalMat, xrayBlueMat, xrayRedMat, xrayYellowMat, xrayPurpleMat, xrayGreenMat);
                SurgeryPanel.SetActive(ActiveSurgeryPanelWhenInOtherMode);
                //SurgeryPanel.transform.SetParent(MannequinMainTransform);
                SetPanelSpawnPosition();

                break;
        }
    }

    private void SetPanelSpawnPosition()
    {
        Vector3 flatForward = Vector3.ProjectOnPlane(_headAnchor.forward, Vector3.up).normalized;
        Quaternion rotation = Quaternion.LookRotation(flatForward);
        Vector3 position = _headAnchor.position + rotation * SpawnOffset;
        rotation = rotation * Quaternion.Euler(-90f, 0f, 0f);
        Pose pose = new Pose(position, rotation);
        SurgeryPanel.transform.SetPose(pose);
    }

    public void Reposition()
    {
        Vector3 flatForward = Vector3.ProjectOnPlane(_headAnchor.forward, Vector3.up).normalized;
        Quaternion rotation = Quaternion.LookRotation(flatForward);
        Vector3 position = _headAnchor.position + rotation * HeadSpawnOffset;
        rotation = rotation * Quaternion.Euler(0f, 0f, 0f);
        Pose pose = new Pose(position, rotation);
        MannequinHead.transform.SetPose(pose);
    }
}
