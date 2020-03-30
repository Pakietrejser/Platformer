using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void SetState(State newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
            Debug.Log(CurrentState);
        }
    }
}