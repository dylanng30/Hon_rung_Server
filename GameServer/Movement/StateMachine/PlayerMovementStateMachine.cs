using GameServer.Movement.States.Player.Airborne;
using GameServer.Movement.States.Player.Airborne.Climbing;
using GameServer.Movement.States.Player.Grounded.Combat.Melee;
using GameServer.Movement.States.Player.Grounded.Crounching;
using GameServer.Movement.States.Player.Grounded.Locomotion;
using GameServer.Movement.States.Player.Grounded.Locomotion.Moving;
using GameServer.Movement.States.Player.Grounded.Stopping;

namespace GameServer.Movement.StateMachine
{
    public class PlayerMovementStateMachine : BaseStateMachine
    {
        public Player Player { get; private set; }
        public IState CurrentState => currentState;


        //Grounded States
        public PlayerIdlingState IdlingState { get; private set; }
        public PlayerWalkingState WalkingState { get; private set; }
        public PlayerRunningState RunningState { get; set; }
        public PlayerSprintingState SprintingState { get; private set; }

        public PlayerLightStoppingState LightStoppingState { get; private set; }
        public PlayerMediumStoppingState MediumStoppingState { get; private set; }
        public PlayerHeavyStoppingState HeavyStoppingState { get; private set; }

        public PlayerCrouchIdlingState CrouchIdlingState { get; private set; }
        public PlayerCrouchWalkingState CrouchWalkingState { get; private set; }

        public PlayerDiagonalAttackState DiagonalAttackState { get; private set; }

        //Airborne States
        public PlayerJumpingState JumpingState { get; private set; }
        public PlayerFallingState FallingState { get; private set; }

        public PlayerClimbIdlingState ClimbIdlingState { get; private set; }
        public PlayerClimbMovingLeftState ClimbMovingLeftState { get; private set; }
        public PlayerClimbMovingRightState ClimbMovingRightState { get; private set; }
        public PlayerClimbMovingUpState ClimbMovingUpState { get; private set; }
        public PlayerClimbMovingDownState ClimbMovingDownState { get; private set; }


        public PlayerMovementStateMachine(Player player)
        {
            Player = player;

            LoadStates();
        }

        private void LoadStates()
        {
            //Grounded States
            IdlingState = new PlayerIdlingState(this);
            WalkingState = new PlayerWalkingState(this);
            RunningState = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);

            LightStoppingState = new PlayerLightStoppingState(this);
            MediumStoppingState = new PlayerMediumStoppingState(this);
            HeavyStoppingState = new PlayerHeavyStoppingState(this);

            CrouchWalkingState = new PlayerCrouchWalkingState(this);
            CrouchIdlingState = new PlayerCrouchIdlingState(this);

            DiagonalAttackState = new PlayerDiagonalAttackState(this);


            //Airborne States
            JumpingState = new PlayerJumpingState(this);
            FallingState = new PlayerFallingState(this);

            ClimbIdlingState = new PlayerClimbIdlingState(this);
            ClimbMovingDownState = new PlayerClimbMovingDownState(this);
            ClimbMovingLeftState = new PlayerClimbMovingLeftState(this);
            ClimbMovingRightState = new PlayerClimbMovingRightState(this);
            ClimbMovingUpState = new PlayerClimbMovingUpState(this);
        }



    }

}
