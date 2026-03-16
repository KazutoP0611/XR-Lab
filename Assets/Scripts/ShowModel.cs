using System.Collections.Generic;
using UnityEngine;

public class ShowModel : MonoBehaviour
{
    private StateMachine stateMachine;

    private Dictionary<int, EntityState> animStates;

    [SerializeField] private Animator anim;

    private void Start()
    {
        
    }

    public void InitModel(Dictionary<int, AnimationData> animationDatas, int currentAnimationIndex)
    {
        stateMachine = new StateMachine();

        animStates = new Dictionary<int, EntityState>();
        for (int i = 0; i < animationDatas.Count; i++)
        {
            EntityState state = new EntityState(anim, animationDatas[i].animParam);
            animStates.Add(i, state);
        }

        stateMachine.Initialize(animStates[currentAnimationIndex]);
    }

    //private void Update()
    //{
    //    stateMachine.UpdateActiveState();
    //}

    public void ChangeAnimation(int currentAnimationIndex)
    {
        EntityState changeToState = animStates[currentAnimationIndex];
        stateMachine.ChangeState(changeToState);
    }
}
