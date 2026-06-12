using UnityEngine;

public class Jump : BaseState
{
    public Jump(PlayerFSM fsm):base(fsm) { }
    public override void OnStateEnter()
    {
        fsm.animator.applyRootMotion = false;
        fsm.animator.SetTrigger("Jump");
        fsm.playerController.Jump();
    }
    public override void Update()
    {
        if (fsm.playerController.VerticalVelocity() < -0.1f)
        {
            fsm.ChangeState(fsm.fallState);
            return;
        }
        else if (fsm.playerController.IsGrounded && fsm.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f&&fsm.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            fsm.ChangeState(fsm.locomotionState);
            fsm.animator.CrossFade("Locomotion", 0.25f);
        }
    }
    public override void FixedUpdate()
    {

    }
    public override void OnStateExit() 
    {
        fsm.animator.applyRootMotion = true;
    }
    
}
