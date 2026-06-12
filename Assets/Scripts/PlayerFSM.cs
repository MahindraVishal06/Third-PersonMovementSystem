using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    public Animator animator;
    public PlayerController playerController;

    public Locomotion locomotionState;
    public Sprint sprintState;
    public CrouchedLocomotion crouchState;
    public Jump jumpState;
    public Fall fallState;
    public Land landState;
    public BaseState currentState;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        locomotionState = new Locomotion(this);
        sprintState = new Sprint(this);
        crouchState=new CrouchedLocomotion(this);
        jumpState = new Jump(this);
        fallState = new Fall(this);
        landState = new Land(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = locomotionState;
    }

    public void ChangeState(BaseState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
}
