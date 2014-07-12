using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionLibrary
{
    /// <summary>
    /// Collision physics engine.
    /// </summary>
    public class Collision
    {
        #region fields
        public List<NextCollision> NextCollisions { get; set; }
        public List<Ball2> Balls { get; set; }
        public List<Wall2> Walls { get; set; }
        private const float MAXTIME = 1000000.0f;
        private const float TIME_TO_NEXT_COLLISION = 1.0f / 60.0f;   // precision of time between each update is done
        #endregion

        #region constructors
        public Collision(List<Ball2> balls, List<Wall2> walls)
        {
            this.NextCollisions = new List<NextCollision>();
            this.NextCollisions.Add(new NextCollision(MAXTIME, new CollisionObject(), new CollisionObject()));
            if (balls != null && walls != null)
            {
                this.Balls = balls;
                this.Walls = walls;
            }
            else
            {
                this.Balls = new List<Ball2>();
                this.Walls = new List<Wall2>();
            }
        }

        public Collision() : this(new List<Ball2>(), new List<Wall2>()) { }
        #endregion fields


        #region collision detection
        public float GetBallWallCollisionTime(Ball2 b, Wall2 w, float directionBallV)
        {
            float s = 0.0f;
            if (w.Orientation == WallOrientation.Bottom || w.Orientation == WallOrientation.Top)
            {
                s = Math.Abs(b.Coordinates.Y - w.Coordinates.Y); 
            }
            else if (w.Orientation == WallOrientation.Left || w.Orientation == WallOrientation.Right)
            {
                s = Math.Abs(b.Coordinates.X - w.Coordinates.X);
            }

            return (s - b.R) / Math.Abs(directionBallV);
        }

        public void GetNextBallWallCollision()
        {
            this.NextCollisions = new List<NextCollision>();
            NextCollision nc = new NextCollision(MAXTIME, new CollisionObject(), new CollisionObject());
            for (int i = 0; i < Balls.Count; i++)
            {
                for (int j = 0; j < Walls.Count; j++)
                {
                    if(Balls[i].V.X > 0 && Walls[j].Orientation == WallOrientation.Right) // ball move in right wall direction
                    {
                        float time = GetBallWallCollisionTime(Balls[i], Walls[j], Balls[i].V.X);
                        if (time >= 0.0f && (time + TIME_TO_NEXT_COLLISION) < nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions = new List<NextCollision>();
                            this.NextCollisions.Add(nc);
                        }
                        else if (time >= 0.0f && (time - TIME_TO_NEXT_COLLISION) <= nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions.Add(nc);
                        }
                    }
                    else if (Balls[i].V.X < 0 && Walls[j].Orientation == WallOrientation.Left) // ball move in left wall direction
                    {
                        float time = GetBallWallCollisionTime(Balls[i], Walls[j], Balls[i].V.X);
                        if (time >= 0.0f && (time + TIME_TO_NEXT_COLLISION) < nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions = new List<NextCollision>();
                            this.NextCollisions.Add(nc);
                        }
                        else if (time >= 0.0f && (time - TIME_TO_NEXT_COLLISION) <= nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions.Add(nc);
                        }
                    }
                    else if (Balls[i].V.Y > 0 && Walls[j].Orientation == WallOrientation.Bottom) // ball move in bottom wall direction
                    {
                        float time = GetBallWallCollisionTime(Balls[i], Walls[j], Balls[i].V.Y);
                        if (time >= 0.0f && (time + TIME_TO_NEXT_COLLISION) < nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions = new List<NextCollision>();
                            this.NextCollisions.Add(nc);
                        }
                        else if (time >= 0.0f && (time - TIME_TO_NEXT_COLLISION) <= nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions.Add(nc);
                        }
                    }
                    else if (Balls[i].V.Y < 0 && Walls[j].Orientation == WallOrientation.Top) // ball move in top wall direction
                    {
                        float time = GetBallWallCollisionTime(Balls[i], Walls[j], Balls[i].V.Y);
                        if (time >= 0.0f && (time + TIME_TO_NEXT_COLLISION) < nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions = new List<NextCollision>();
                            this.NextCollisions.Add(nc);
                        }
                        else if (time >= 0.0f && (time - TIME_TO_NEXT_COLLISION) <= nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions.Add(nc);
                        }
                    }

                }
            }

        }

        public void GetNextBallBallCollision()
        {
            float curr_time;
            if (NextCollisions.Count > 0) curr_time = NextCollisions[0].Time;
            else curr_time = MAXTIME;
            NextCollision nc = new NextCollision(curr_time, new CollisionObject(), new CollisionObject());
            for (int i = 0; i < Balls.Count; i++)
            {
                for (int j = i + 1; j < Balls.Count; j++)
                {
                    float xdiff = Balls[j].Coordinates.X - Balls[i].Coordinates.X;
                    float ydiff = Balls[j].Coordinates.Y - Balls[i].Coordinates.Y;
                    float vxdiff = Balls[j].V.X - Balls[i].V.X;
                    float vydiff = Balls[j].V.Y - Balls[i].V.Y;
                    float xdiff2 = xdiff * xdiff;
                    float ydiff2 = ydiff * ydiff;
                    float vxdiff2 = vxdiff * vxdiff;
                    float vydiff2 = vydiff * vydiff;
                    float r_sum = Balls[i].R + Balls[j].R;
                    float r_sum2 = r_sum * r_sum;
                    float a = vxdiff2 + vydiff2;
                    float b = 2.0f * xdiff * vxdiff + 2.0f * ydiff * vydiff;
                    float c = xdiff2 + ydiff2 - r_sum2;     
                    QuadraticEquation qe = new QuadraticEquation(a, b, c);
                    float time = qe.CalcSmallerPositiveX();
                    if ((float)Math.Round((decimal)time, 3) > 0.0f && (time + TIME_TO_NEXT_COLLISION) < nc.Time)   
                    {
                        nc = new NextCollision(time, Balls[i], Balls[j]);
                        this.NextCollisions = new List<NextCollision>();
                        this.NextCollisions.Add(nc);
                    }
                    else if ((float)Math.Round((decimal)time, 3) > 0.0f && (time - TIME_TO_NEXT_COLLISION) <= nc.Time)
                    {
                        nc = new NextCollision(time, Balls[i], Balls[j]);
                        this.NextCollisions.Add(nc);
                    }
                }
            }

        }
        #endregion collision detection

        #region collision calculations
        /// <summary>
        /// Move each ball to time [s] (uniform rectlinear motion)
        /// </summary>
        /// <param name="t"></param>
        public void MoveBallsToTime(float t)
        {
            foreach (Ball2 b in Balls)
            {
                b.MoveBallUniformRectilinearMotion(t);
            }
        }

        private Ball2 CalcPostImpactVBallWall(Ball2 b, Wall2 w)
        {
            if (w.Orientation == WallOrientation.Right || w.Orientation == WallOrientation.Left)
            {
                b.V = new Vector2(-b.V.X, b.V.Y);
            }
            else if (w.Orientation == WallOrientation.Top || w.Orientation == WallOrientation.Bottom)
            {
                b.V = new Vector2(b.V.X, -b.V.Y);
            }
            return b;
        }

        /// <summary>
        /// Returns post-impact velocities as a NextCollision object
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        public NextCollision CalcPostImpactVBallWall(NextCollision nc)
        {
            if (nc != null && nc.Obj1 != null && nc.Obj2 != null)
            {
                if (nc.Obj1.M < CollisionObject.WALL_MASS && nc.Obj2.M == CollisionObject.WALL_MASS)
                {
                    Ball2 b = (Ball2)nc.Obj1;
                    Wall2 w = (Wall2)nc.Obj2;
                    nc.Obj1 = CalcPostImpactVBallWall(b, w);
                }
                else if (nc.Obj1.M == CollisionObject.WALL_MASS && nc.Obj2.M < CollisionObject.WALL_MASS)
                {
                    Ball2 b = (Ball2)nc.Obj2;
                    Wall2 w = (Wall2)nc.Obj1;
                    nc.Obj2 = CalcPostImpactVBallWall(b, w);
                }
            }
            return nc;
        }

        /// <summary>
        /// Returns post-impact velocities as a NextCollision object
        /// </summary>
        /// <param name="nc"></param>
        /// <returns></returns>
        public NextCollision CalcPostImpactVBallBall(NextCollision nc)
        {
            if (nc != null && nc.Obj1 != null && nc.Obj2 != null)
            {
                if (nc.Obj1.M < CollisionObject.WALL_MASS && nc.Obj2.M < CollisionObject.WALL_MASS)
                {
                    Ball2 b1 = (Ball2)nc.Obj1;
                    Ball2 b2 = (Ball2)nc.Obj2;

                    Vector2 normal = Transformation.CalcNormal(b1, b2);
                    Vector2 tangent = Transformation.CalcTangent(normal);

                    float v1n = Transformation.Projection(b1.V, normal);
                    float v1t = Transformation.Projection(b1.V, tangent);
                    float v2n = Transformation.Projection(b2.V, normal);
                    float v2t = Transformation.Projection(b2.V, tangent);

                    float massSum = b1.M + b2.M;
                    float factor1 = b1.M / massSum;
                    float factor2 = b2.M / massSum;

                    float v1nPost = factor1 * v1n - factor2 * v1n + 2 * factor2 * v2n;
                    float v2nPost = -factor1 * v2n + factor2 * v2n + 2 * factor1 * v1n;

                    b1.V = new Vector2(v1nPost * normal.X + v1t * tangent.X, v1nPost * normal.Y + v1t * tangent.Y);
                    b2.V = new Vector2(v2nPost * normal.X + v2t * tangent.X, v2nPost * normal.Y + v2t * tangent.Y);

                    return new NextCollision(nc.Time, b1, b2);
                }
            }
            return nc;
        }
        #endregion collision calculations

        #region others
        public float CalcTotalKineticEnergy()
        {
            float total_kin_energy = 0.0f;
            foreach (Ball2 b in Balls)
            {
                float v_2 = b.V.X * b.V.X + b.V.Y * b.V.Y;
                total_kin_energy += 0.5f * b.M * v_2;
            }
            return total_kin_energy;
        }
        #endregion
    }
}
