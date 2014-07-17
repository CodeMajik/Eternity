using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Eternity
{
    public static class CollisionFunctions
    {
        public static bool CubeOverlapping(ref Entity a, ref Entity b)
        {
            double minDistX = (a.m_width/2.0) + (b.m_width/2.0);
            double minDistY = (a.m_height / 2.0) + (b.m_height / 2.0);
            double actDistX = Math.Abs(a.GetCenterX() - b.GetCenterX());
            double actDistY = Math.Abs(a.GetCenterY() - b.GetCenterY());

            return (actDistX <= minDistX) && (actDistY <= minDistY);
        }

        public static bool EntitiesWithinDistance(ref Entity a, ref Entity b, double distance)
        {
            return Vector2.Distance(a.GetCenter(), b.GetCenter()) <= distance;
        }

        public static double DistanceBetweenEntityMidpoints(ref Entity a, ref Entity b)
        {
            return Vector2.Distance(a.GetCenter(), b.GetCenter());
        }

        public static Vector2 GetCollisionDirection(ref Entity a, ref Entity b)
        {
            bool onLeft = a.GetCenterX() <= b.GetCenterX();
            bool onRight = a.GetCenterX() >= b.GetCenterX();
            bool above = a.GetBotY() <= b.GetTopY();
            bool below = a.GetTopY() >= b.GetBotY();
            double dirX = 0.0;
            double dirY = 0.0;
            if (onLeft)
                dirX = -1.0;
            if (onRight)
                dirX = 1.0;
            if (above)
                dirY = 1.0;
            if (below)
                dirY = -1.0;

            return new Vector2((float)dirX, (float)dirY);
        }
    }
}
