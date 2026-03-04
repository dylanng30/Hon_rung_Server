using System.Numerics;
using GameServer.Movement.StateMachine;
using GameServer.Physics;

namespace GameServer.Movement.States.Player.Airborne
{
    public class PlayerAirborneState : PlayerMovementState
    {
        public PlayerAirborneState(PlayerMovementStateMachine playerStateMachine) : base(playerStateMachine)
        {
            _stateMachine = playerStateMachine;
        }

        public override void Enter()
        {
            base.Enter();

            //StartAnimation(_stateMachine.Player.AnimationData.AirborneParameterHash);
        }
        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);

            _inputDirection = Vector2.Zero;
            if (movementInputs[0]) _inputDirection.Y += 1;
            if (movementInputs[1]) _inputDirection.Y -= 1;
            if (movementInputs[2]) _inputDirection.X += 1;
            if (movementInputs[3]) _inputDirection.X -= 1;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Move();            
        }

        public override void Exit()
        {
            base.Exit();
            //StopAnimation(_stateMachine.Player.AnimationData.AirborneParameterHash);
        }

        protected bool IsClimbing()
        {
            var player = _stateMachine.Player;

            float reachDistance = 1.0f;
            Vector3 faceDirection = Vector3.Transform(Vector3.UnitZ, player.rotation);
            Vector3 origin = player.position + new Vector3(0, Constants.PlayerHeight * 0.5f, 0);
            Vector3 targetPos = origin + faceDirection * reachDistance;

            Vector3 checkSize = new Vector3(0.5f, 0.5f, 0.5f);
            return CollisionWorld.CheckOverlap(targetPos, checkSize);
        }

        protected void CheckGround()
        {
            var player = _stateMachine.Player;
            float groundY = CollisionWorld.GetGroundY(player.position, Constants.PlayerCollisionRadius);
            if (player.position.Y <= groundY)
            {
                player.position.Y = groundY;
                player.VelocityY = 0;

                if (_inputDirection != Vector2.Zero)
                    _stateMachine.ChangeState(_stateMachine.RunningState);
                else
                    _stateMachine.ChangeState(_stateMachine.IdlingState);
            }
        }

        protected void AddGravity(float deltaTime)
        {
            var player = _stateMachine.Player;
            player.VelocityY -= Constants.Gravity * deltaTime;
            player.position.Y += player.VelocityY * deltaTime;
        }
    }
}
