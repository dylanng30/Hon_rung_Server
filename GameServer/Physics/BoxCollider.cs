using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Physics;

public struct BoxCollider
{
    public Vector3 Min;
    public Vector3 Max;

    public BoxCollider(Vector3 position, Vector3 size)
    {
        Vector3 halfSize = size / 2;
        Min = position - halfSize;
        Max = position + halfSize;
    }

    public bool Intersects(BoxCollider other)
    {
        bool overlapX = Min.X <= other.Max.X && Max.X >= other.Min.X;
        bool overlapY = Min.Y <= other.Max.Y && Max.Y >= other.Min.Y;
        bool overlapZ = Min.Z <= other.Max.Z && Max.Z >= other.Min.Z;
        return overlapX && overlapY && overlapZ;
    }

    public bool CheckCollision(Vector3 playerPos, float radius)
    {
        if (playerPos.Y >= Max.Y - 0.05f) return false;

        if (playerPos.Y + 1.8f <= Min.Y) return false;

        return IsTouchingXZ(playerPos, radius);
    }

    public bool IsInsideXZ(Vector3 pos)
    {
        return pos.X >= Min.X && pos.X <= Max.X &&
               pos.Z >= Min.Z && pos.Z <= Max.Z;
    }

    public bool IsTouchingXZ(Vector3 playerPos, float radius)
    {
        float closestX = Math.Clamp(playerPos.X, Min.X, Max.X);
        float closestZ = Math.Clamp(playerPos.Z, Min.Z, Max.Z);

        float distanceX = playerPos.X - closestX;
        float distanceZ = playerPos.Z - closestZ;

        return (distanceX * distanceX + distanceZ * distanceZ) < (radius * radius);
    }
}
