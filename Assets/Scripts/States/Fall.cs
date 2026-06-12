using UnityEngine;

public class Fall : BaseState
{
    public Fall(PlayerFSM fsm):base(fsm) { }
    public override void OnStateEnter()
    {
        fsm.animator.applyRootMotion = false;
        fsm.animator.SetBool("IsFalling", true);
    }
    public override void Update()
    {
        if (fsm.playerController.IsGrounded)
        {
            fsm.ChangeState(fsm.landState);
        }
    }
    public override void FixedUpdate()
    {

    }
    public override void OnStateExit() 
    {
        fsm.animator.applyRootMotion = true;
        fsm.animator.SetBool("IsFalling", false);
    }
    
}
