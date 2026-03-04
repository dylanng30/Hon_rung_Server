using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Airborne
{
    public class PlayerJumpingState : PlayerAirborneState
    {
        public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _stateMachine.Player.MoveSpeed = Constants.BaseSpeed * Constants.RunSpeedModifer / Constants.TICKS_PER_SEC;
            StartAnimation("Jump");
            StartJumping();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            AddGravity(deltaTime);
            CheckGround();
            if (_stateMachine.Player.VelocityY < 0)
            {
                _stateMachine.ChangeState(_stateMachine.FallingState);
            }
        }


        public override void Exit()
        {
            base.Exit();
            StopAnimation("Jump");
        }


        private void StartJumping()
        {
            //TODO Logic
            var player = _stateMachine.Player;
            player.VelocityY = Constants.JumpForce;
        }
    }
}
