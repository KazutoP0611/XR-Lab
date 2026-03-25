using System.Collections.Generic;
using UnityEngine;

public class MainShowModelController : MonoBehaviour
{
    private ShowModel showModel;
    private int currentAnimationIndex;
    
    private bool animIsPlaying = false;
    private GameObject playerGameObject;

    private ModelMatMode currentModelMateriMode;
    private bool freeMode = false;
    private bool cutMode = false;
    private bool realWorldMode = false;
    private bool giantSize = false;

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
            Debug.LogError("\"Animation View\" components has not been referenced yet.");

        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        currentModelMateriMode = ModelMatMode.NormalMat;

        // Prepare the animation data and show model before initializing the title view.
        SetAnimationDataDictionary();

        // Instantiate show model.
        InstantiateShowModel();
        SetShowModelTransform(false);

        // Initialize the title view after preparing the animation data and show model.
        // So now title view is showing the current animation.
        animationView?.InitTitleView(this);
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
        GameObject model = Instantiate(modeldata.modelPrefab);
        showModel = model.GetComponent<ShowModel>();
        showModel.InitModel(animationDataDict, currentAnimationIndex);
    }

    private void SetShowModelTransform(bool giantSize)
    {
        if (giantSize)
        {
            showModel.transform.position = modeldata.giantSizePosition;
            showModel.transform.rotation = Quaternion.Euler(modeldata.giantSizeRotate);
            showModel.transform.localScale = modeldata.giantSizeScale;
        }
        else
        {
            Vector3 handPosition = playerGameObject.transform.position + showModel.transform.forward * modeldata.smallSizePositionOffset.z;
            handPosition.y = modeldata.smallSizePositionOffset.y;
            showModel.transform.position = handPosition;

            showModel.transform.rotation = Quaternion.Euler(modeldata.smallSizeRotate);
            showModel.transform.localScale = modeldata.smallSizeScale;
        }
    }

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
        freeMode = !freeMode;

        SetModelModeAndChangeMaterials(ModelMatMode.NormalMat);
        showModel.SetMovableComponents(freeMode);
    }

    public void ToggleXRayMode()
    {
        SetModelModeAndChangeMaterials(ModelMatMode.XrayMat);

        cutMode = false;
        cutPanelObject.SetActive(false);
    }

    public void TogglePanelObject()
    {
        cutMode = !cutMode;

        SetModelModeAndChangeMaterials(cutMode ? ModelMatMode.CutMat : ModelMatMode.NormalMat);
        cutPanelObject.SetActive(cutMode);
    }

    public void ToggleGiantSizeMode()
    {
        SetModelModeAndChangeMaterials(cutMode ? ModelMatMode.CutMat : ModelMatMode.NormalMat);

        // Disable panel button in real size mode;
        giantSize = !giantSize;
        animationView.EnablePanelObjectButton(giantSize);
    }

    private void SetModelModeAndChangeMaterials(ModelMatMode modelMode)
    {
        currentModelMateriMode = currentModelMateriMode == modelMode ? ModelMatMode.NormalMat : modelMode;
        showModel.SetModelMaterialFromMode(currentModelMateriMode);
    }

    public void ToggleRealWorldView()
    {
        // Toggle skybox and MR components;
        realWorldMode = !realWorldMode;
    }
}
