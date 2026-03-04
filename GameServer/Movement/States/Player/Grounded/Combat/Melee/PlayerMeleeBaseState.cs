using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Combat.Melee
{
    public class PlayerMeleeBaseState : PlayerCombatState
    {
        public PlayerMeleeBaseState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }
    }
}
