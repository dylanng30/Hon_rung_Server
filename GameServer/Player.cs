using System.Numerics;
using GameServer.Movement.StateMachine;
using GameServer.Physics;

namespace GameServer;

public class Player
{
    public int id;
    public string username;

    public Vector3 position;
    public Quaternion CameraRotation;
    public Quaternion rotation;

    public float Height;

    public float MoveSpeed;
    public float RotateSpeed;
    public bool[] inputs;

    public float VelocityY;
    public bool IsGrounded => position.Y <= CollisionWorld.GetGroundY(position, Constants.PlayerCollisionRadius) + 0.05f;


    public bool PressingW => inputs[0];
    public bool PressingS => inputs[1];
    public bool PressingA => inputs[2];
    public bool PressingD => inputs[3];
    public bool PressingSpace => inputs[4];
    public bool PressingLeftControl => inputs[5];
    public bool PressedLeftShift => inputs[6];
    public bool PressLeftMouse => inputs[7];


    public int MatchId = 0;
    public bool IsReadyToPlay = false;


    private PlayerMovementStateMachine _stateMachine;

    public Player(int _id, string _username, Vector3 _spawnPosition)
    {
        id = _id;
        username = _username;
        position = _spawnPosition;
        CameraRotation = Quaternion.Identity;
        rotation = Quaternion.Identity;


        RotateSpeed = Constants.BaseRotateSpeed / Constants.TICKS_PER_SEC;

        inputs = new bool[8];

        _stateMachine = new PlayerMovementStateMachine(this);
        _stateMachine.ChangeState(_stateMachine.IdlingState);
    }

    public void Update(float _deltaTime)
    {
        _stateMachine.HandleInput(inputs);
        _stateMachine.Update(_deltaTime);

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    public void SetInput(bool[] _inputs, Quaternion _camRotation)
    {
        inputs = _inputs;
        CameraRotation = _camRotation;
    }

    public void OnAnimationEnterEvent()
    {
        //Console.Write("OnAnimationEnterEvent");
        _stateMachine.OnAnimationEnterEvent();
    }
    public void OnAnimationTransitionEvent()
    {
        //Console.Write("OnAnimationTransitionEvent");
        _stateMachine.OnAnimationTransitionEvent();
    }
    public void OnAnimationExitEvent()
    {
        //Console.Write("OnAnimationExitEvent");
        _stateMachine.OnAnimationExitEvent();
    }

    public bool IsMoving()
    {
        return PressingW || PressingS || PressingA || PressingD;
    }
}

