using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Airborne.Climbing
{
    public class PlayerClimbUpState : PlayerClimbingState
    {
        public PlayerClimbUpState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {

        }
    }
}
