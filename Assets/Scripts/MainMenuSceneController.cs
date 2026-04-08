using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        FadePanelController.Instance.FadeToBlack(() => {
            SceneManager.LoadScene(sceneName);
        });
    }
}
