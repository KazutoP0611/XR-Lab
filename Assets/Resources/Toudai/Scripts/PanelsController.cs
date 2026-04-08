using Oculus.Interaction.Locomotion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelsController : MonoBehaviour
{
    [Header("Guide Panel Setting")]
    public Transform GuidePanelSpawnPoint;
    public GuidePanelController GuidePanelController;

    [Header("Setting Panel Setting")]
    public MenuToggler SettingMenuToggler;
    public string HomeScene;

    [Header("Panels")]
    public Panel ReadyPanel;
    [SerializeField] private Transform readyPanelTransform;

    private bool GuidePanelIsActive = false;

    private void Start()
    {
        InitGuidePanel();
    }

    private void InitGuidePanel()
    {
        // Init guide panel to the first page and hide it at the start of the scene.
        GuidePanelController.SetGuidePanelPageIndicator(0);

        GuidePanelIsActive = false;
        GuidePanelController.TogglePanel(GuidePanelIsActive);
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
            //SettingMenuToggler.HidePanel();

            ReadyPanel.transform.position = readyPanelTransform.position;
            ReadyPanel.transform.rotation = readyPanelTransform.rotation;
            ReadyPanel.OpenPanel();
        }
        else
        {
            //ReadyPanel.SetOnCloseAnimationEndEvent(() =>
            //{
            //    SettingMenuToggler.ShowPanel();
            //});

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

        MRPassthoughController.instance.TurnOnPassthough(false);
        ReadyPanel.ClosePanel();
    }
}
