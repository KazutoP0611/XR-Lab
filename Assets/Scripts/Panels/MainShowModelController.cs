using Oculus.Interaction.Samples;
using System.Collections.Generic;
using UnityEngine;

public class MainShowModelController : MonoBehaviour
{
    private ShowModel showModel;
    private GameObject playerGameObject;
    private MRPassthrough mrPassthrough;
    private int currentAnimationIndex;
    private bool animIsPlaying = false;

    private ModelMatMode currentModelMateriMode;
    private bool freeMode = false;
    private bool cutMode = false;
    private bool realWorldMode = false;
    private bool giantSize = false;

    public Dictionary<int, AnimationData> animationDataDict { get; private set; }
    public AnimationData[] animationDatas { get; private set; }

    [Header("General Details")]
    public ModelData_SO modeldata;

    [Header("Animation View Details")]
    [SerializeField] private AnimationView animationView;

    [Header("Cut Panel Details")]
    public GameObject cutPanelObject;

    private void Start()
    {
        if (animationView == null)
            Debug.LogError("\"Animation View\" components has not been referenced yet.");

        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        mrPassthrough = GetComponent<MRPassthrough>();

        currentModelMateriMode = ModelMatMode.NormalMat;

        // Prepare the animation data and show model before initializing the title view.
        SetAnimationDataDictionary();

        // Instantiate show model.
        InstantiateShowModel();

        // Initialize the title view after preparing the animation data and show model.
        // So now title view is showing the current animation.
        animationView?.InitTitleView(this);

        // Play animation when scene starts.
        PlayPauseAnimation();

        // Set its position to small size's position
        SetShowModelTransform(false);
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
        showModel.InitModel(this);
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
            Vector3 smallSizePosition = playerGameObject.transform.position + playerGameObject.transform.forward * modeldata.smallSizePositionOffset.z;
            smallSizePosition.x = modeldata.smallSizePositionOffset.x;
            smallSizePosition.y = modeldata.smallSizePositionOffset.y;
            showModel.transform.position = smallSizePosition;

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

    #region Model Mode Controller
    public void ToggleFreeMode()
    {
        freeMode = !freeMode;

        showModel.SetMovableComponents(freeMode);
    }

    public void ToggleXRayMode()
    {
        // Disable cut mode
        cutMode = false;
        cutPanelObject.SetActive(false);

        // Change model's material
        SetModelModeAndChangeMaterials(ModelMatMode.XrayMat);
    }

    public void TogglePanelObject()
    {
        // Activate cut panel
        cutMode = !cutMode;
        cutPanelObject.SetActive(cutMode);

        // Change model's material
        SetModelModeAndChangeMaterials(cutMode ? ModelMatMode.CutMat : ModelMatMode.NormalMat);

        // Set enable panel control component
        showModel.ActivateCutShader(cutMode);
    }

    public void ToggleRealWorldView()
    {
        // Toggle skybox and MR components;
        realWorldMode = !realWorldMode;
        mrPassthrough.TogglePassThrough();
    }

    public void ToggleGiantSizeMode()
    {
        // Close cut panel, turn off cut mode
        if (cutMode == true)
            animationView.SwitchCutMode(false);

        if (freeMode == true)
            animationView.SwitchFreeMode(false);

        // Giant Size Logic
        //---------------------------------
        giantSize = !giantSize;

        // Change model material to normal material
        //SetModelModeAndChangeMaterials(ModelMatMode.NormalMat);

        // Disable panel buttons in giant size mode;
        animationView.EnableFreeModeButton(!giantSize);
        animationView.EnablePanelObjectButton(!giantSize);

        // Enlarge model size
        SetShowModelTransform(giantSize);
        //---------------------------------
    }
    #endregion

    private void SetModelModeAndChangeMaterials(ModelMatMode modelMode)
    {
        currentModelMateriMode = currentModelMateriMode == modelMode ? ModelMatMode.NormalMat : modelMode;
        showModel.SetModelMaterialFromMode(currentModelMateriMode);
    }
}
