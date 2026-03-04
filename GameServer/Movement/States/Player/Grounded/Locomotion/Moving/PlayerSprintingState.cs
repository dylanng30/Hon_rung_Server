using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Locomotion.Moving
{
    public class PlayerSprintingState : PlayerMovingState
    {
        public PlayerSprintingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            // Logic for entering the sprinting state
            //StartAnimation(SprintingParameterHash);
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Logic to update the sprinting state
        }
        public override void Exit()
        {
            base.Exit();
            // Logic for exiting the sprinting state
            //StopAnimation(SprintingParameterHash);
        }
    }
}
