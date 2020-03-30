namespace FSM
{
    public abstract class State
    {
        protected readonly StateMachine stateMachine;
        protected readonly Player controller;

        protected State(StateMachine stateMachine, Player controller)
        {
            this.stateMachine = stateMachine;
            this.controller = controller;
        }
        
        public virtual void Enter() {}
        
        public virtual void LogicUpdate() {}
        
        public virtual void PhysicsUpdate() {}
        
        public virtual void Exit() {}
    }
}