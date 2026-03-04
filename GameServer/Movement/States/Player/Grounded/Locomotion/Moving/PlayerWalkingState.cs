using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Locomotion.Moving
{
    public class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();

            //Set up data
            _stateMachine.Player.MoveSpeed = Constants.BaseSpeed * Constants.WalkSpeedModifier / Constants.TICKS_PER_SEC;

            // Logic for entering the walking state
            StartAnimation("Walk");
        }

        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);

            if (!_stateMachine.Player.PressingLeftControl)
            {
                _stateMachine.ChangeState(_stateMachine.RunningState);
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Logic to update the walking state
        }

        public override void Exit()
        {
            base.Exit();
            // Logic for exiting the walking state
            StopAnimation("Walk");
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            // Transition to another state if needed
        }
    }
}
