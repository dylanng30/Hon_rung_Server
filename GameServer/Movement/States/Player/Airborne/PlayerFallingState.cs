using System.Numerics;
using GameServer.Movement.StateMachine;
using GameServer.Physics;

namespace GameServer.Movement.States.Player.Airborne
{
    public class PlayerFallingState : PlayerAirborneState
    {
        public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _stateMachine.Player.MoveSpeed = Constants.BaseSpeed * Constants.RunSpeedModifer / Constants.TICKS_PER_SEC;
            //StartAnimation("Fall");
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            AddGravity(deltaTime);
            CheckGround();

            if (IsClimbing())
            {
                _stateMachine.ChangeState(_stateMachine.ClimbIdlingState);
                return;
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            //StopAnimation("Fall");
        }
    }
}
