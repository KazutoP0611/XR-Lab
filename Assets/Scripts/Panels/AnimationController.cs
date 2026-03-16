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

        SetAnimationDataDictionary();
        InstantiateShowModel();

        animationView.InitTitleView(this);
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

    public void PlayPauseAnimation()
    {
        animIsPlaying = !animIsPlaying;
        animationView.SetPlayButton(animIsPlaying);
    }

    public void NextAnimation()
    {
        currentAnimationIndex++;
        animationView.PlayTitleAnimation(AnimationToPlay.Right, ChangeModelAnimation);
        CurrenAnimationIndexCheck();
    }

    public void PreviousAnimation()
    {
        currentAnimationIndex--;
        animationView.PlayTitleAnimation(AnimationToPlay.Left, ChangeModelAnimation);
        CurrenAnimationIndexCheck();
    }

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
