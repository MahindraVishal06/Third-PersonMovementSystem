using UnityEngine;

public class CrouchedLocomotion : BaseState
{
    public CrouchedLocomotion(PlayerFSM fsm):base(fsm) { }
    public override void OnStateEnter()
    {
        fsm.animator.SetBool("IsCrouched", true);
        //fsm.playerController.defaultCam.SetActive(false);
        //fsm.playerController.crouchCam.SetActive(true);
    }
    public override void Update()
    {
        Vector2 input = fsm.playerController.GetInp();
        //fsm.animator.SetFloat("WalkSpeed", Mathf.Clamp01(input.magnitude));
        if(!fsm.playerController.IsCrouched || fsm.playerController.IsJump)
        {
            fsm.ChangeState(fsm.locomotionState);
        }
        if (!fsm.playerController.IsGrounded && fsm.playerController.VerticalVelocity() < -0.2f)
        {
            fsm.ChangeState(fsm.fallState);
        }
    }
    public override void FixedUpdate()
    {
        fsm.playerController.HandlePlayerRotation();
    }
    public override void OnStateExit() 
    {
        fsm.playerController.IsCrouched = false;
        fsm.animator.SetBool("IsCrouched", false);
        //fsm.playerController.crouchCam.SetActive(false);
       //fsm.playerController.defaultCam.SetActive(true) ;
    }
    
}
