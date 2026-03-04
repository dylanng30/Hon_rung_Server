using System.Numerics;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Airborne.Climbing
{
    public class PlayerClimbIdlingState : PlayerClimbingState
    {
        public PlayerClimbIdlingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            
        }

    
    }
}
