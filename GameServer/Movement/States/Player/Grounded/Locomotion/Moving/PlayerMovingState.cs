using System.Numerics;
using GameServer.Movement.StateMachine;


namespace GameServer.Movement.States.Player.Grounded.Locomotion.Moving
{
    public class PlayerMovingState : PlayerGroundedState
    {
        protected bool[] _movementInputs;
        
        public PlayerMovingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            //StartAnimation(MovingParameterHash);
        }
        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);
            _movementInputs = movementInputs;

            if(_inputDirection == Vector2.Zero)
            {
                _stateMachine.ChangeState(_stateMachine.IdlingState);
                return;
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Move();
        }

        public override void Exit()
        {
            base.Exit();
            //StopAnimation(MovingParameterHash);
        }
    }
}
