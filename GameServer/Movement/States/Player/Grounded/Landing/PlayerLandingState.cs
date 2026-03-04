using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Landing
{
    public class PlayerLandingState : PlayerMovementState
    {
        public PlayerLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            // Logic for entering the landing state
            //StartAnimation(LandingParameterHash);
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Logic to update the landing state
        }
        public override void Exit()
        {
            base.Exit();
            // Logic for exiting the landing state
            //StopAnimation(LandingParameterHash);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationEnterEvent();

            // Transition to IdlingState
        }


    }
}
