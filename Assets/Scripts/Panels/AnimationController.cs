using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private ShowModel showModel;
    private bool animIsPlaying = false;
    private int currentAnimationIndex;

    private Dictionary<int, AnimationData> animationDataDict;

    public AnimationData[] animationDatas { get; private set; }

    [SerializeField] private AnimationView animationView;

    [Header("General Details")]
    public ModelData_SO modeldata;

    private void Start()
    {
        if (animationView == null)
            Debug.LogWarning("\"Animation View\" components has not been referenced yet.");

        // Prepare the animation data and show model before initializing the title view.
        SetAnimationDataDictionary();
        InstantiateShowModel();

        // Initialize the title view after preparing the animation data and show model.
        // So now title view is showing the current animation.
        animationView.InitTitleView(this);

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

    private void ChangeModelAnimation()
    {
        showModel.ChangeAnimation(currentAnimationIndex);
    }

    #region Animation's Controller
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

    public string GetCurrentAnimationTitle() => animationDataDict[currentAnimationIndex].animationTitle;

    public int GetCurrentAnimationIndex() => currentAnimationIndex;
}
