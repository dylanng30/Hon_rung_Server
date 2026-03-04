using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Crounching
{
    public class PlayerCrouchWalkingState : PlayerCrounchingState
    {
        public PlayerCrouchWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation("CrouchWalk");

            _stateMachine.Player.MoveSpeed = Constants.BaseSpeed * Constants.WalkSpeedModifier / Constants.TICKS_PER_SEC;
        }

        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);

            if (_stateMachine.Player.PressedLeftShift)
            {
                _stateMachine.ChangeState(_stateMachine.IdlingState);
            }
            else if (!_stateMachine.Player.IsMoving())
            {
                _stateMachine.ChangeState(_stateMachine.CrouchIdlingState);
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            Move();
        }

    }
}
