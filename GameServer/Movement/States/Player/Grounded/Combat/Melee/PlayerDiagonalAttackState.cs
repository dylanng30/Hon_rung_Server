using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Grounded.Combat.Melee
{
    public class PlayerDiagonalAttackState : PlayerMeleeBaseState
    {
        private float animTimer;
        public PlayerDiagonalAttackState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation("DiagonalAttack");
            animTimer = 2f;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (animTimer > 0)
            {
                animTimer -= deltaTime;
            }
            else
            {
                
                _stateMachine.ChangeState(_stateMachine.IdlingState);
            }
            
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();            
        }

        public override void OnAnimationExitEvent()
        {
            base.OnAnimationExitEvent();
            _stateMachine.ChangeState(_stateMachine.IdlingState);
        }
    }
}
