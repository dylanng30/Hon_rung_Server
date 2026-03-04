using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Stopping
{
    public class PlayerHeavyStoppingState : PlayerStoppingState
    {
        public PlayerHeavyStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            // Logic for entering the heavy stopping state
            //StartAnimation(HeavyStoppingParameterHash);
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Logic to update the heavy stopping state
        }
        public override void Exit()
        {
            base.Exit();
            // Logic for exiting the heavy stopping state
            //StopAnimation(HeavyStoppingParameterHash);
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            // Transition to IdlingState
        }
    }
}
