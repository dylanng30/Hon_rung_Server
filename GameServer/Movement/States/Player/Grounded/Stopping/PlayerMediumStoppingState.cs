using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Stopping
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            // Logic for entering the medium stopping state
            //StartAnimation(MediumStoppingParameterHash);
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Logic to update the medium stopping state
        }
        public override void Exit()
        {
            base.Exit();
            // Logic for exiting the medium stopping state
            //StopAnimation(MediumStoppingParameterHash);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            // Transition to IdlingState
        }
    }
}
