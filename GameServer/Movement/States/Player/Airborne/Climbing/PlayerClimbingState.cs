using System.Numerics;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Airborne.Climbing
{
    public class PlayerClimbingState : PlayerAirborneState
    {
        public PlayerClimbingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            // Additional logic for entering climbing state
        }
        public override void Update(float deltaTime)
        {
            //base.Update(deltaTime);

            if(_inputDirection == Vector2.Zero)
            {
                _stateMachine.ChangeState(_stateMachine.ClimbIdlingState);
                return;
            }

            ClimbMove(deltaTime);
        }
        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);
            // Additional logic for climbing state input handling
        }
        public override void Exit()
        {
            base.Exit();
            // Additional logic for exiting climbing state
        }

        protected void ClimbMove(float deltaTime)
        {
            var player = _stateMachine.Player;
            player.position.Y += _inputDirection.Y * Constants.Gravity * deltaTime;

            if (_inputDirection.X > 0) _stateMachine.ChangeState(_stateMachine.ClimbMovingRightState);
            else if (_inputDirection.X < 0) _stateMachine.ChangeState(_stateMachine.ClimbMovingLeftState);
            else if (_inputDirection.Y > 0) _stateMachine.ChangeState(_stateMachine.ClimbMovingUpState);
            else if (_inputDirection.Y < 0) _stateMachine.ChangeState(_stateMachine.ClimbMovingDownState);
        }


    }
}
