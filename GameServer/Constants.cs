using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer;

class Constants
{
    public const int TICKS_PER_SEC = 60;
    public const int MS_PER_TICK = 1000 / TICKS_PER_SEC;

    public const int DataBufferSize = 4096;

    //Base data enities
    // Movement
    public const float BaseSpeed = 10f;
    public const float BaseRotateSpeed = 10f;

    public const float WalkSpeedModifier = 0.225f;
    public const float RunSpeedModifer = 1f;
    public const float SprintSpeedModifier = 1.7f;

    public const float Gravity = 9.81f * 2f;
    public const float JumpForce = 10f;


    //Collision
    public const float PlayerCollisionRadius = 1f;
    public const float PlayerHeight = 2f;
}