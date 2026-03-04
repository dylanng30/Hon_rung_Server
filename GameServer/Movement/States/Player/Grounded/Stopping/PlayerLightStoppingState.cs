using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Stopping
{
    public class PlayerLightStoppingState : PlayerStoppingState
    {
        public PlayerLightStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            // Logic for entering the light stopping state
            //StartAnimation(LightStoppingParameterHash);
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Logic to update the light stopping state
        }
        public override void Exit()
        {
            base.Exit();
            // Logic for exiting the light stopping state
            //StopAnimation(LightStoppingParameterHash);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            // Transition to IdlingState
        }
    }
}
