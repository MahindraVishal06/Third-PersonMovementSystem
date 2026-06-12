using UnityEngine;

public class Land : BaseState
{
    public Land(PlayerFSM fsm):base(fsm) { }
    public override void OnStateEnter()
    {
        
    }
    public override void Update()
    {
        if (fsm.animator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
        {
            if (fsm.playerController.IsSprint && fsm.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                fsm.ChangeState(fsm.sprintState);
                return;
            }
            else if(fsm.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                fsm.ChangeState(fsm.locomotionState);
        }
    }
    public override void FixedUpdate()
    {

    }
    public override void OnStateExit() { }
    
}
