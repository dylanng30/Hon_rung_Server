using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded
{
    public class PlayerGroundedState : PlayerMovementState
    {

        public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _stateMachine.Player.Height = Constants.PlayerHeight;
        }
        
        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);

            if (_stateMachine.Player.PressingSpace)
            {
                _stateMachine.ChangeState(_stateMachine.JumpingState);
            }
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (!_stateMachine.Player.IsGrounded)
            {
                _stateMachine.ChangeState(_stateMachine.FallingState);
                return;
            }        
        }

        public override void Exit()
        {
            base.Exit();
            //StopAnimation(GroundedParameterHash);
        }
    }
}
