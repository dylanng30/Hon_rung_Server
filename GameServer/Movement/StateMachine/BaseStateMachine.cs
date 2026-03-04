namespace GameServer.Movement.StateMachine
{
    public abstract class BaseStateMachine
    {
        protected IState currentState;

        public void ChangeState(IState newState)
        {
            currentState?.Exit();

            currentState = newState;

            currentState?.Enter();
        }

        public void HandleInput(bool[] movementInputs)
        {
            currentState?.HandleInput(movementInputs);
        }

        public void Update(float deltaTime)
        {
            currentState?.Update(deltaTime);
        }

        public void OnAnimationEnterEvent()
        {
            currentState?.OnAnimationEnterEvent();
        }
        public void OnAnimationExitEvent()
        {
            currentState?.OnAnimationExitEvent();
        }
        public void OnAnimationTransitionEvent()
        {
            currentState?.OnAnimationTransitionEvent();
        }
    }
}
