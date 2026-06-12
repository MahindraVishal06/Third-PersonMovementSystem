using UnityEngine;

[System.Serializable]
public abstract class BaseState 
{
    protected PlayerFSM fsm;
    public BaseState(PlayerFSM fsm) {  this.fsm = fsm; }
    public abstract void OnStateEnter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void OnStateExit();

}
