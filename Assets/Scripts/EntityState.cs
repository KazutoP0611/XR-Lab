using UnityEngine;

public class EntityState
{
    private Animator anim;
    private string animParam;

    public EntityState(Animator anim, string animParam)
    {
        this.anim = anim;
        this.animParam = animParam;
    }

    public virtual void Enter()
    {
        Debug.Log($"Entered {animParam} state.");
        anim.SetBool(animParam, true);
    }

    public virtual void Update()
    { }

    public virtual void Exit()
    {
        anim.SetBool(animParam, false);
    }
}
