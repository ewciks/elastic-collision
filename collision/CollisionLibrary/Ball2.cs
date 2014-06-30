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
    /// Ball2 class - objects participating in the collision
    /// Two dimensions.
    /// </summary>
    public class Ball2 : CollisionObject
    {
        #region fields
        public Vector2 V { get; set; }              // velocity
        public float M { get; set; }                // mass
        public float R { get; set; }                // radius
        #endregion

        #region constructors
        public Ball2(Vector2 vel, Vector2 coord, float mass, float rad)
        {
            this.V = vel;
            this.Coordinates = coord;
            this.M = mass;
            this.R = rad;
        }

        /// <summary>
        /// Default constructor with 'zero' values
        /// </summary>
        public Ball2() : this(Vector2.Zero, Vector2.Zero, 0.0f, 0.0f) { }
        
        /// <summary>
        /// Stationary material point without mass, with given coordinates
        /// </summary>
        /// <param name="coord"></param>
        public Ball2(Vector2 coord) : this(Vector2.Zero, coord, 0.0f, 0.0f) { }
        #endregion

        #region overrides
        public override string ToString()
        {
            return "V: (" + this.V.X + ", " + this.V.Y
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
        #endregion
    }
}
