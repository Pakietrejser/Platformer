namespace FSM.PlayerStates
{
    public class GroundedState : PlayerState
    {
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (isJumping)
                stateMachine.SetState(controller.jumping);
            
            if (!controller.Col.OnGround)
                stateMachine.SetState(controller.airbone);
        }

        public GroundedState(StateMachine stateMachine, Player controller) : base(stateMachine, controller) { }
    }
}