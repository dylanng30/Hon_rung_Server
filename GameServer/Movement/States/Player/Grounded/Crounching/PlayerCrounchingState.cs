using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Crounching
{
    public class PlayerCrounchingState : PlayerGroundedState
    {
        public PlayerCrounchingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            var player = _stateMachine.Player;
            player.Height = Constants.PlayerHeight * 0.5f;
        }

        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);
        }

    }
}
