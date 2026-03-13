using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelsController : MonoBehaviour
{
    //public MenuToggler GuidePanelMenuToggler;
    [Header("Guide Panel Setting")]
    public Transform GuidePanelSpawnPoint;
    public GuidePanelController GuidePanelController;

    [Header("Setting Panel Setting")]
    public MenuToggler SettingMenuToggler;
    public string HomeScene;

    [Header("Panels")]
    public Panel ReadyPanel;

    private bool GuidePanelIsActive = false;

    private void Start()
    {
        //GuidePanelMenuToggler.HidePanel();
        GuidePanelController.SetIndicator(0);
        GuidePanelController.TogglePanel(false);
        GuidePanelIsActive = false;
    }

    public void SetActiveGuidePanel()
    {
        GuidePanelIsActive = !GuidePanelIsActive;
        if (GuidePanelIsActive)
        {
            GuidePanelController.gameObject.transform.position = GuidePanelSpawnPoint.position;
        }

        GuidePanelController.TogglePanel(GuidePanelIsActive);
    }

    public void SetActiveReadypanel(bool active)
    {
        if (active)
        {
            SettingMenuToggler.HidePanel();
            ReadyPanel.OpenPanel();
        }
        else
        {
            ReadyPanel.SetOnCloseAnimationEndEvent(() =>
            {
                SettingMenuToggler.ShowPanel();
            });

            ReadyPanel.ClosePanel();
        }
    }

    public void QuitEducation()
    {
        ReadyPanel.SetOnCloseAnimationEndEvent(() =>
        {
            FadePanelController.Instance.FadeToBlack(() => {
                SceneManager.LoadScene(HomeScene);
            });
        });

        ReadyPanel.ClosePanel();
    }
}
