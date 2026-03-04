using System.Collections.Generic;
using System.Numerics;

namespace GameServer.Physics;

public static class CollisionWorld
{
    public static List<BoxCollider> Obstacles = new List<BoxCollider>();

    public static void Initialize()
    {
        Obstacles.Add(new BoxCollider(new Vector3(5, 0.5f, 5), new Vector3(2, 1, 2)));
        Obstacles.Add(new BoxCollider(new Vector3(5, 2.5f, -5), new Vector3(1, 5, 5)));
        Obstacles.Add(new BoxCollider(new Vector3(-5, 2.5f, -5), new Vector3(1, 5, 5)));
    }

    public static bool CheckOverlap(Vector3 position, Vector3 size)
    {
        BoxCollider checkBox = new BoxCollider(position, size);
        foreach (var box in Obstacles)
        {
            if (box.Intersects(checkBox))
                return true;
        }

        return false;
    }


    public static Vector3 MoveWithCollision(Vector3 currentPos, Vector3 velocity, float playerRadius)
    {
        Vector3 targetX = currentPos + new Vector3(velocity.X, 0, 0);
        if (!IsColliding(targetX, playerRadius)) 
            currentPos.X = targetX.X;

        Vector3 targetZ = currentPos + new Vector3(0, 0, velocity.Z);
        if (!IsColliding(targetZ, playerRadius)) 
            currentPos.Z = targetZ.Z;

        return currentPos;
    }

    public static float GetGroundY(Vector3 pos, float radius)
    {
        float groundY = 0f;

        foreach (var box in Obstacles)
        {
            if (box.IsTouchingXZ(pos, radius))
            {
                if (pos.Y >= box.Min.Y)
                {
                    if (box.Max.Y > groundY)
                    {
                        groundY = box.Max.Y;
                    }
                }
            }
        }
        return groundY;
    }

    private static bool IsColliding(Vector3 pos, float radius)
    {
        foreach (var box in Obstacles)
        {
            if (box.CheckCollision(pos, radius)) return true;
        }
        return false;
    }
}