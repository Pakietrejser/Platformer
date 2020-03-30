using FSM;
using FSM.PlayerStates;

public class Player : CharacterController2D
{
    StateMachine sm;
    
    public State grounded;
    public State airbone;
    public State onWall;
    public State jumping;
    public State dashing;

    void Start()
    {
        sm = new StateMachine();
        grounded = new GroundedState(sm, this);
        airbone = new AirboneState(sm, this);
        onWall = new OnWallState(sm, this);
        jumping = new JumpingState(sm, this);
        dashing = new DashingState(sm, this);
        
        sm.SetState(airbone);
    }

    void Update()
    {
        sm.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        sm.CurrentState.PhysicsUpdate();
    }
}
