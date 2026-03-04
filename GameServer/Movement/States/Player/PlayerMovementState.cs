using System.Numerics;
using GameServer.Movement.StateMachine;
using GameServer.Physics;

namespace GameServer.Movement.States.Player
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine _stateMachine;

        protected Vector2 _inputDirection;
        protected bool _isCrounching = false;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            _stateMachine = playerMovementStateMachine;
        }
        public virtual void Enter()
        {
            //Console.WriteLine($"[Player {_stateMachine.Player.id}] Enter {this.GetType().Name}");
        }

        public virtual void Exit()
        {
            //Console.WriteLine($"[Player {_stateMachine.Player.id}] Exit {this.GetType().Name}");
        }

        public virtual void HandleInput(bool[] movementInputs)
        {
            _inputDirection = Vector2.Zero;
            if (movementInputs[0]) _inputDirection.Y += 1;
            if (movementInputs[1]) _inputDirection.Y -= 1;
            if (movementInputs[2]) _inputDirection.X += 1;
            if (movementInputs[3]) _inputDirection.X -= 1;
        }
        public virtual void Update(float deltaTime)
        {

        }

        public virtual void OnAnimationEnterEvent()
        {
            //Console.WriteLine($"{this.GetType().Name} is on animation enter event");
        }

        public virtual void OnAnimationExitEvent()
        {
            //Console.WriteLine($"{this.GetType().Name} is on animation exit event");
        }

        public virtual void OnAnimationTransitionEvent()
        {
            //Console.WriteLine($"{this.GetType().Name} is on animation transition event");
        }


        #region --- HELPER METHODS ---
        protected virtual void StartAnimation(string animName)
        {
            var player = _stateMachine.Player;
            ServerSend.PlayerDoAnimation(player, animName);                
        }
        protected virtual void StopAnimation(string animName)
        {
            // Implementation for stopping an animation
        }
        protected virtual void StartAnimation(int animationHash)
        {
            // Implementation for starting an animation
        }
        protected virtual void StopAnimation(int animationHash)
        {
            // Implementation for stopping an animation
        }

        protected virtual void Move()
        {
            if (_inputDirection == Vector2.Zero)
                return;

            var player = _stateMachine.Player;
            Vector3 moveDirection = GetMoveDirection(player.CameraRotation);
            Vector3 velocity = moveDirection * player.MoveSpeed;
            Vector3 finalPosition = CollisionWorld.MoveWithCollision(player.position, velocity, Constants.PlayerCollisionRadius);

            player.position.X = finalPosition.X;
            player.position.Z = finalPosition.Z;

            Vector3 lookDirection = new Vector3(moveDirection.X, 0, moveDirection.Z);

            if (lookDirection.LengthSquared() > 0.0001f)
            {
                lookDirection = Vector3.Normalize(lookDirection);
                float yaw = (float)Math.Atan2(lookDirection.X, lookDirection.Z);
                player.rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, yaw);
            }            
        }

        protected Vector3 GetMoveDirection(Quaternion cameraRotation)
        {
            Vector3 forward = Vector3.Transform(new Vector3(0, 0, 1), cameraRotation);
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, new Vector3(0, 1, 0)));

            Vector3 moveDirection = right * _inputDirection.X + forward * _inputDirection.Y;
            moveDirection.Y = 0;
            return moveDirection;
        }
        #endregion


    }
}
