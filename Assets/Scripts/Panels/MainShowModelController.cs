using System.Collections.Generic;
using UnityEngine;

public class MainShowModelController : MonoBehaviour
{
    private ShowModel showModel;
    private int currentAnimationIndex;
    
    private bool animIsPlaying = false;
    private bool xrayModeIsOn = false;
    private bool handsizeMode = true;

    private Dictionary<int, AnimationData> animationDataDict;

    public AnimationData[] animationDatas { get; private set; }

    [SerializeField] private AnimationView animationView;

    [Header("General Details")]
    public ModelData_SO modeldata;

    [Header("Cut Panel Details")]
    [SerializeField] private GameObject cutPanelObject;

    private void Start()
    {
        if (animationView == null)
            Debug.LogWarning("\"Animation View\" components has not been referenced yet.");

        // Prepare the animation data and show model before initializing the title view.
        SetAnimationDataDictionary();

        // Instantiate show model.
        InstantiateShowModel();

        // Initialize the title view after preparing the animation data and show model.
        // So now title view is showing the current animation.
        animationView.InitTitleView(this);
        // TODO Set up buttons.

        // Play animation when scene starts.
        PlayPauseAnimation();
    }

    private void SetAnimationDataDictionary()
    {
        animationDatas = modeldata.animationDatas;

        animationDataDict = new Dictionary<int, AnimationData>();
        for (int i = 0; i < animationDatas.Length; i++)
        {
            animationDataDict.Add(i, animationDatas[i]);
        }

        currentAnimationIndex = 0;
    }

    public void InstantiateShowModel()
    {
        GameObject model = Instantiate(modeldata.modelPrefab, modeldata.spawnPosition, Quaternion.Euler(modeldata.spawnAngle));
        showModel = model.GetComponent<ShowModel>();
        showModel.InitModel(animationDataDict, currentAnimationIndex);
    }

    public string GetCurrentAnimationTitle() => animationDataDict[currentAnimationIndex].animationTitle;

    public int GetCurrentAnimationIndex() => currentAnimationIndex;

    #region Animation Panel View's Controller
    public void PlayPauseAnimation()
    {
        animIsPlaying = !animIsPlaying;
        animationView.SetPlayButton(animIsPlaying);

        showModel.SetAnimationSpeed(animIsPlaying);
    }

    public void ToLeftAnimation()
    {
        currentAnimationIndex--;
        animationView.PlayTitleAnimation(PanelAnimation.Left, ChangeModelAnimation);
        CurrenAnimationIndexCheck();
    }

    public void ToRightAnimation()
    {
        currentAnimationIndex++;
        animationView.PlayTitleAnimation(PanelAnimation.Right, ChangeModelAnimation);
        CurrenAnimationIndexCheck();
    }
    #endregion

    private void CurrenAnimationIndexCheck()
    {
        if (currentAnimationIndex < 0)
            currentAnimationIndex = animationDataDict.Count - 1;
        else if (currentAnimationIndex >= animationDataDict.Count)
            currentAnimationIndex = 0;  
    }

    private void ChangeModelAnimation()
    {
        showModel.ChangeAnimation(currentAnimationIndex);
    }

    public void ToggleFreeMode()
    {
        // Disable hand size mode button, and change show model to small size;
    }

    public void ToggleXRayMode()
    {
        xrayModeIsOn = !xrayModeIsOn;
        showModel.SetModelXrayMode(xrayModeIsOn);
    }

    public void TogglePanelObject()
    {
        xrayModeIsOn = false;
        showModel.SetModelXrayMode(xrayModeIsOn);

        // Set active panel object
        cutPanelObject.SetActive(!cutPanelObject.activeInHierarchy);
        // Change material of the model to the "Cut shader" material.
    }

    public void ToggleRealWorldView()
    {
        // Toggle skybox and MR components;
    }

    public void ToggleHandSizeModel()
    {
        // Disable panel button in real size mode;
        handsizeMode = !handsizeMode;
        animationView.EnablePanelObjectButton(handsizeMode);
    }
}
