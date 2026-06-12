using UnityEngine;

public class Sprint : BaseState
{
    public Sprint(PlayerFSM fsm):base(fsm) { }
    public override void OnStateEnter()
    {
        fsm.animator.SetBool("IsRunning", true);
        fsm.playerController.stepSmoothness = 1.2f;
    }
    public override void Update()
    {
        Vector2 input = fsm.playerController.GetInp();
        if(input.magnitude<0.1 || !fsm.playerController.IsSprint)
        {
            fsm.ChangeState(fsm.locomotionState);
        }
        if (fsm.playerController.IsCrouched)
        {
            fsm.ChangeState(fsm.crouchState);
        }
        if (fsm.playerController.IsJump)
        {
            fsm.ChangeState(fsm.jumpState);
        }
        if (!fsm.playerController.IsGrounded && fsm.playerController.VerticalVelocity() < -0.2f)
        {
            fsm.ChangeState(fsm.fallState);
        }
        if (fsm.playerController.turn180&&!fsm.playerController.animationBusy)
        {
            fsm.animator.SetBool("Turn 180",true);
            fsm.playerController.animationBusy = true;
        }
        /*if (fsm.animator.GetCurrentAnimatorStateInfo(0).IsName("RunTurn180") && fsm.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            fsm.playerController.turn180 = false;
            fsm.playerController.animationBusy = false;
        }*/
    }
    public override void FixedUpdate()
    {
        fsm.playerController.HandlePlayerRotation();
    }
    public override void OnStateExit() 
    {
        fsm.animator.SetBool("IsRunning", false);
    }
    
}
