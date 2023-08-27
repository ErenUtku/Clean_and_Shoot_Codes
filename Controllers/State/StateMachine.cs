namespace Controllers.State
{
    public class StateMachine
    {
        public BaseState currentState;

        public void Initialize()
        {
            // Initial state (CollectState)
            currentState = new CollectState();
            currentState.EnterState();
        }

        public void Update()
        {
            currentState.UpdateState();
        }
        
        public void ChangeState(BaseState newState)
        {
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }
    }
}