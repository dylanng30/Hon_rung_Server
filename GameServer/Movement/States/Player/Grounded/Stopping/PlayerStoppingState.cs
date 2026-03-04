using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Stopping
{
    public class PlayerStoppingState : PlayerGroundedState
    {
        public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            // Logic for entering the stopping state
            //StartAnimation(StoppingParameterHash);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Logic to update the stopping state
        }

        public override void Exit()
        {
            base.Exit();
            // Logic for exiting the stopping state
            //StopAnimation(StoppingParameterHash);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            // Transition to IdlingState
        }
    }
}
