using Oculus.Interaction.Samples;
using UnityEngine;

public class MRPassthoughController : MonoBehaviour
{
    private MRPassthrough mrPassthrough;

    public static MRPassthoughController instance { get; private set; }

    private void Start()
    {
        AssignInstance();

        mrPassthrough = GetComponent<MRPassthrough>();
    }

    private void AssignInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Created "toggle" and "turn on or off" seperately for easilyof usage;
    public void ToggleWorldView() => mrPassthrough.TogglePassThrough();

    public void TurnOnPassthough(bool turnOn)
    {
        if (turnOn == true)
            mrPassthrough.TurnPassThroughOn();
        else
            mrPassthrough.TurnPassThroughOff();
    }
    //---------------------------------------------------------------------
}
