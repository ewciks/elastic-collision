using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionLibrary
{
    /// <summary>
    /// Public enum with representation of wall direction.
    /// </summary>
    public enum WallOrientation { Right, Top, Left, Bottom };

    /// <summary>
    /// Two dimensional horizontal or vertical wall with id, default mass and coordinates
    /// </summary>
    public class Wall2 : CollisionObject
    {
        #region fields
        /// <summary>
        /// Wall orientation.
        /// </summary>
        public WallOrientation Orientation { get; set; }
        #endregion fields

        #region constructors
        /// <summary>
        /// Regular two dimensional wall constructor with coordinates and orientation.
        /// </summary>
        /// <param name="coord">Two dimensional vector of coordinates in [m].</param>
        /// <param name="orientation">Two dimentional vertical or horizontal wall orientation.</param>
        public Wall2(Vector2 coord, WallOrientation orientation)
        {
            this.M = CollisionObject.WALL_MASS;
            this.Coordinates = coord;
            if (orientation != null) this.Orientation = orientation;
            else this.Orientation = WallOrientation.Right;
        }

        /// <summary>
        /// Default constructor of a horizontal wall in left-top corner.
        /// </summary>
        public Wall2() : this(Vector2.Zero, WallOrientation.Top) { }
        #endregion
    }
}
