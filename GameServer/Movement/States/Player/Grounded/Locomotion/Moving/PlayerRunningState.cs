using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Locomotion.Moving
{
    public class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();

            //Set up data
            _stateMachine.Player.MoveSpeed = Constants.BaseSpeed * Constants.RunSpeedModifer / Constants.TICKS_PER_SEC;

            StartAnimation("Run");
        }
        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);

            if(_stateMachine.Player.PressingLeftControl)
            {
                _stateMachine.ChangeState(_stateMachine.WalkingState);
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            // Logic to update the running state
        }
        public override void Exit()
        {
            base.Exit();
            StopAnimation("Run");
        }
    }
}
