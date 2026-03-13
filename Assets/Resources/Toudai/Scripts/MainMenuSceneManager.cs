using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    [Header("Panels")]
    public Panel SplashScreen;
    public Panel MainMenuPanel;
    public Panel ReadyToPlayPanel;
    public Panel QuitMenuPanel;

    [Header("Scenes")]
    public string TutorialScene;
    public string EducationScene;

    private void Start()
    {
        SplashScreen.SetOnCloseAnimationEndEvent(() =>
        {
            MainMenuPanel.OpenPanel();
        });

        SplashScreen.OpenPanel();
    }

    public void StartGame()
    {
        //set something when close animation ends
        MainMenuPanel.SetOnCloseAnimationEndEvent(() => {
            ReadyToPlayPanel.OpenPanel();
        });

        MainMenuPanel.ClosePanel();
    }

    public void GotoTutorialScene()
    {
        ReadyToPlayPanel.SetOnCloseAnimationEndEvent(() => {
            FadePanelController.Instance.FadeToBlack(() =>
            {
                SceneManager.LoadScene(TutorialScene);
            });
        });

        ReadyToPlayPanel.ClosePanel();
    }   
    
    public void GotoEducationScene()
    {
        ReadyToPlayPanel.SetOnCloseAnimationEndEvent(() => {
            FadePanelController.Instance.FadeToBlack(() =>
            {
                SceneManager.LoadScene(EducationScene);
            });
        });

        ReadyToPlayPanel.ClosePanel();
    }

    public void ToggleQuitMenu(bool active)
    {
        if (active)
        {
            MainMenuPanel.SetOnCloseAnimationEndEvent(() => {
                QuitMenuPanel.OpenPanel();
            });

            MainMenuPanel.ClosePanel();
        }
        else
        {
            QuitMenuPanel.SetOnCloseAnimationEndEvent(() => {
                MainMenuPanel.OpenPanel();
            });

            QuitMenuPanel.ClosePanel();
        }
    }    

    public void ExitApplication()
    {
        Application.Quit();
    }
}
