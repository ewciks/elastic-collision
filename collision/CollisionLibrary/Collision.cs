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
        public List<NextCollision> NextCollisions { get; set; }
        public List<Ball2> Balls { get; set; }
        public List<Wall2> Walls { get; set; }

        private const float MAXTIME = 1000000.0f;
        #endregion

        #region constructors
        public Collision(List<Ball2> balls, List<Wall2> walls)
        {
            this.NextCollisions = new List<NextCollision>();
            this.NextCollisions.Add(new NextCollision(MAXTIME, new CollisionObject(), new CollisionObject()));
            this.Balls = balls;
            this.Walls = walls;
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

            return (s - b.R) / directionBallV;
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
                        if (time < nc.Time) 
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions = new List<NextCollision>();
                            this.NextCollisions.Add(nc);
                        }
                        else if (time == nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions.Add(nc);
                        }
                    }
                    else if (Balls[i].V.X < 0 && Walls[j].Orientation == WallOrientation.Left) // ball move in left wall direction
                    {
                        float time = GetBallWallCollisionTime(Balls[i], Walls[j], Balls[i].V.X);
                        if (time < nc.Time) 
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions = new List<NextCollision>();
                            this.NextCollisions.Add(nc);
                        }
                        else if (time == nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions.Add(nc);
                        }
                    }
                    else if (Balls[i].V.Y > 0 && Walls[j].Orientation == WallOrientation.Bottom) // ball move in bottom wall direction
                    {
                        float time = GetBallWallCollisionTime(Balls[i], Walls[j], Balls[i].V.Y);
                        if (time < nc.Time) 
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions = new List<NextCollision>();
                            this.NextCollisions.Add(nc);
                        }
                        else if (time == nc.Time)
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions.Add(nc);
                        }
                    }
                    else if (Balls[i].V.Y < 0 && Walls[j].Orientation == WallOrientation.Top) // ball move in top wall direction
                    {
                        float time = GetBallWallCollisionTime(Balls[i], Walls[j], Balls[i].V.Y);
                        if (time < nc.Time) 
                        {
                            nc = new NextCollision(time, Balls[i], Walls[j]);
                            this.NextCollisions = new List<NextCollision>();
                            this.NextCollisions.Add(nc);
                        }
                        else if (time == nc.Time)
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
            NextCollision nc = new NextCollision(NextCollisions[0].Time, new CollisionObject(), new CollisionObject());
            for (int i = 0; i < Balls.Count; i++)
            {
                for (int j = i; j < Balls.Count; j++)
                {
                    float xdiff = Balls[j].Coordinates.X - Balls[i].Coordinates.X;
                    float ydiff = Balls[j].Coordinates.Y - Balls[i].Coordinates.Y;
                    float vxdiff = Balls[j].V.X - Balls[i].V.X;
                    float vydiff = Balls[j].V.Y - Balls[i].V.Y;
                    float a = vxdiff * vxdiff + vydiff * vydiff;
                    float b = 2 * xdiff * vxdiff + 2 * ydiff * vydiff;
                    float c = xdiff * xdiff + ydiff * ydiff - (float)Math.Pow(Balls[j].R + Balls[i].R, 2);
                    QuadraticEquation qe = new QuadraticEquation(a, b, c);
                    float time = qe.CalcSmallerPositiveX();
                    if (time >= 0 && time < nc.Time)
                    {
                        nc = new NextCollision(time, Balls[i], Balls[j]);
                        this.NextCollisions = new List<NextCollision>();
                        this.NextCollisions.Add(nc);
                    }
                    else if (time == nc.Time)
                    {
                        nc = new NextCollision(time, Balls[i], Balls[j]);
                        this.NextCollisions.Add(nc);
                    }
                }
            }

        }
        #endregion collision detection

    }
}
