using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollisionLibrary
{
    /// <summary>
    /// Collision engine
    /// </summary>
    public class Collision
    {
        #region fields
        public float TNextCollition { get; set; }
        public int NextCollisionBallId1 { get; set; }
        public int NextCollisionBallId2 { get; set; }
        public int NextCollisionWallId { get; set; }
        public bool IsNextCollisionBallBall { get; set; }
        #endregion

        public void GetNextBallCollision(List<Ball2> balls)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i; j < balls.Count; j++)
                {
                    float xdiff = balls[j].Coordinates.X - balls[i].Coordinates.X;
                    float ydiff = balls[j].Coordinates.Y - balls[i].Coordinates.Y;
                    float vxdiff = balls[j].V.X - balls[i].V.X;
                    float vydiff = balls[j].V.Y - balls[i].V.Y;
                    float a = vxdiff * vxdiff + vydiff * vydiff;
                    float b = 2 * xdiff * vxdiff + 2 * ydiff * vydiff;
                    float c = xdiff * xdiff + ydiff * ydiff - (float)Math.Pow(balls[j].R + balls[i].R, 2);
                    QuadraticEquation qe = new QuadraticEquation(a, b, c);
                    float tnext = qe.CalcSmallerPositiveX();
                    if (tnext >= 0 && tnext < TNextCollition)
                    {
                        TNextCollition = tnext;
                        // TO DO, ids
                    }
                }
            }

        }
    }
}
