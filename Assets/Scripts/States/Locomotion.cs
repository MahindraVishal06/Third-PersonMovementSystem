using System;
using UnityEngine;

public class Locomotion : BaseState
{
    public Locomotion(PlayerFSM fsm):base(fsm) { }
    public override void OnStateEnter()
    {
        fsm.playerController.stepSmoothness = 0.8f;
    }
    public override void Update()
    {
        Vector2 input = fsm.playerController.GetInp();
        //fsm.animator.SetFloat("WalkSpeed", Mathf.Clamp01(input.magnitude));
        if(input.magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            fsm.ChangeState(fsm.sprintState);
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
            fsm.animator.SetBool("Turn 180", true);
            fsm.playerController.animationBusy = true;
        }
        /*if (fsm.animator.GetCurrentAnimatorStateInfo(0).IsName("walkTurn180") && fsm.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            fsm.playerController.turn180 = false;
            fsm.playerController.animationBusy = false;
        }*/
    }
    public override void FixedUpdate()
    {
        fsm.playerController.HandlePlayerRotation();
    }
    public override void OnStateExit() { }
    
}
