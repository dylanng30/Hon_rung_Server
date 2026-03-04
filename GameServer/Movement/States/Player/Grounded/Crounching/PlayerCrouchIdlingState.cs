using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Crounching
{
    public class PlayerCrouchIdlingState : PlayerCrounchingState
    {
        public PlayerCrouchIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation("CrouchIdle");
        }

        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);

            if (_stateMachine.Player.PressedLeftShift)
            {
                _stateMachine.ChangeState(_stateMachine.IdlingState);
            }
            else if (_stateMachine.Player.IsMoving())
            {
                _stateMachine.ChangeState(_stateMachine.CrouchWalkingState);
            }
        }

    }
}
