using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace FSM.PlayerStates
{
    public class AirboneState : PlayerState
    {
        float fallMultiplier = 2.5f;
        float lowJumpMultiplier = 2f;
        readonly Vector2 gravity = Vector2.up * Physics2D.gravity.y * Time.deltaTime;

        int maxJumpsInTheAir = 2;
        int currentJumpsInTheAir;

        bool readyToCheckCollision;

        public override void Enter()
        {
            controller.Jump();
            currentJumpsInTheAir = 0;
            Delay();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isJumping && currentJumpsInTheAir < maxJumpsInTheAir)
            {
                ++currentJumpsInTheAir;
                controller.AirboneJump();
            }
            
            if (controller.Col.OnGround && readyToCheckCollision)
                stateMachine.SetState(controller.grounded);
            
            if (controller.Col.OnWall && readyToCheckCollision)
                stateMachine.SetState(controller.onWall);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            if (controller.Rb.velocity.y < 0 )
                controller.AddVelocity(gravity * fallMultiplier);
            else if (!Input.GetButton("Jump") && currentJumpsInTheAir == 0)
                controller.AddVelocity(gravity * lowJumpMultiplier);
        }

        async Task Delay()
        {
            readyToCheckCollision = false;
            await Task.Delay(300);
            readyToCheckCollision = true;
        }

        public AirboneState(StateMachine stateMachine, Player controller) : base(stateMachine, controller) { }
    }
}