using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CollisionLibrary
{
    /// <summary>
    /// Two dimensional ball with id, mass, coordinates, velocity and radius
    /// </summary>
    public class Ball2 : CollisionObject
    {
        #region fields
        /// <summary>
        /// Two dimensional vector of velocities in [m/s].
        /// </summary>
        public Vector2 V { get; set; }
        /// <summary>
        /// Unnegative radius in [m].
        /// </summary>
        public float R { get; set; }
        #endregion fields

        #region constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vel">Two dimensional vector of velocities in [m/s].</param>
        /// <param name="coord">Two dimensional vector of coordinates in [m].</param>
        /// <param name="mass">Unnegative mass in [kg].</param>
        /// <param name="rad">Unnegative radius in [m].</param>
        public Ball2(Vector2 vel, Vector2 coord, float mass, float rad)
        {
            this.V = vel;
            this.Coordinates = coord;
            if (mass >= CollisionObject.WALL_MASS || mass <= 0.0f) this.M = 0.001f;
            else this.M = mass;
            if (rad <= 0.0f) this.R = 0.001f; 
            else this.R = rad;
        }

        /// <summary>
        /// Default constructor with 'zero' values.
        /// </summary>
        public Ball2() : this(Vector2.Zero, Vector2.Zero, 0.0f, 0.0f) { }
        
        /// <summary>
        /// Stationary material point without mass, with given coordinates
        /// </summary>
        /// <param name="coord"></param>
        public Ball2(Vector2 coord) : this(Vector2.Zero, coord, 0.0f, 0.0f) { }
        #endregion constructors

        #region move
        /// <summary>
        /// Move ball with it's velocity in uniform recrtilinear motion for time t.
        /// </summary>
        /// <param name="t">Time in seconds.</param>
        public void MoveBallUniformRectilinearMotion(float t)
        {
            this.Coordinates += this.V * t;
        }
        #endregion move

        #region overrides
        public override string ToString()
        {
            return "ID: [" + this.Id + "], V: (" + this.V.X + ", " + this.V.Y
                + "), coord: (" + this.Coordinates.X + ", " + this.Coordinates.Y + "), "
                + "mass: " + this.M + ", radius: " + this.R;
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Ball2 p = obj as Ball2;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (V == p.V) && (Coordinates == p.Coordinates) && (M == p.M) && (R == p.R);
        }

        public bool Equals(Ball2 p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (V == p.V) && (Coordinates == p.Coordinates) && (M == p.M) && (R == p.R);
        }

        public override int GetHashCode()
        {
            return (int)(V.X * V.Y * Coordinates.X * Coordinates.Y * M * R);
        }
        #endregion overrides
    }
}
