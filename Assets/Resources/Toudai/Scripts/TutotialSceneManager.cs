using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutotialSceneManager : MonoBehaviour
{
    public List<Interactor> ListOfTutorial;
    public GameObject ShowTutorialEnd;
    public Panel TutorialEndPanel;
    public string EducationScene;

    private int tutorialCount = 0;
    private bool tutorialEnd = false;

    private void Start()
    {
        ShowTutorialEnd.SetActive(false);
        FadePanelController.Instance.FadeToNormal(() =>
        {
            PlayTutorials();
        });
    }

    private void PlayTutorials()
    {
        if (!tutorialEnd)
        {
            ListOfTutorial[tutorialCount].Init(CheckPlayedTutorial);
        }
        else
        {
            //ShowTutorialEnd.SetActive(true);
            TutorialEndPanel.OpenPanel();
        }
    }

    public void GotoEducationScene()
    {
        FadePanelController.Instance.FadeToBlack(() =>
        {
            SceneManager.LoadScene(EducationScene);
        });
    }    

    private void CheckPlayedTutorial()
    {
        tutorialCount++;
        if (tutorialCount >= ListOfTutorial.Count)
        {
            tutorialEnd = true;
        }

        PlayTutorials();
    }
}
