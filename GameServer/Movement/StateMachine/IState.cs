using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Movement.StateMachine;

public interface IState
{
    void Enter();
    void HandleInput(bool[] movementInputs);
    void Update(float deltaTime);
    void Exit();


    //Event
    void OnAnimationEnterEvent();
    void OnAnimationExitEvent();
    void OnAnimationTransitionEvent();
}
