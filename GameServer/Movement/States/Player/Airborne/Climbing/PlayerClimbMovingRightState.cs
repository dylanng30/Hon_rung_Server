using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Airborne.Climbing
{
    public class PlayerClimbMovingRightState : PlayerClimbingState
    {
        public PlayerClimbMovingRightState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (_inputDirection == Vector2.Zero)
            {
                _stateMachine.ChangeState(_stateMachine.ClimbIdlingState);
                return;
            }
        }

    }
}
