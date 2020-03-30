using UnityEngine;

namespace FSM.PlayerStates
{
    public abstract class PlayerState : State
    {
        protected Vector2 inputDireciton;
        protected bool isDashing;
        protected bool isJumping;

        void HandleInput()
        {
            inputDireciton = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            isJumping = Input.GetButtonDown("Jump");
            isDashing = Input.GetButtonDown("Dash");
            
            if (inputDireciton.x > 0 && controller.CurrentSpeed < 0 || inputDireciton.x < 0 && controller.CurrentSpeed > 0)
                controller.ResetCurrentSpeed();
        }
        
        public override void LogicUpdate()
        {
            HandleInput();
        }

        public override void PhysicsUpdate()
        {
            controller.MoveHorizontally(inputDireciton);
        }

        protected PlayerState(StateMachine stateMachine, Player controller) : base(stateMachine, controller) { }
    }
}