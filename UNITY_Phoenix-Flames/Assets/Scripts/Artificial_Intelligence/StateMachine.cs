using System;
using System.Reflection;

namespace Artificial_Intelligence
{
    public class StateMachine
    {
        public AIState currentState;
        public Action<AIState> OnStateChanged;

        public void SwitchState(AIState state)
        {
            currentState = state;
            OnStateChanged?.Invoke(currentState);
            Logs.Log(GetType(), MethodBase.GetCurrentMethod(), $"State has switch and it's now {state}.");
        }
    }

    public enum AIState
    {
        OnPatrol,
        OnFollow,
        OnChase,
        OnAttack
    }
}