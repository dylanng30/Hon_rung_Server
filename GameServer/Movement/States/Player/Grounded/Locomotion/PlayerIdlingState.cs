using GameServer.Movement.StateMachine;


namespace GameServer.Movement.States.Player.Grounded.Locomotion
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation("Idle");
        }

        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);

            if (_stateMachine.Player.PressLeftMouse)
            {
                _stateMachine.ChangeState(_stateMachine.DiagonalAttackState);
            }
            else if (_stateMachine.Player.PressedLeftShift)
            {
                _stateMachine.ChangeState(_stateMachine.CrouchIdlingState);
            }
            else if (_stateMachine.Player.IsMoving())
            {
                _stateMachine.ChangeState(_stateMachine.RunningState);
            }
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            
        }
        public override void Exit()
        {
            base.Exit();
            StopAnimation("Idle");
        }
    }
}
