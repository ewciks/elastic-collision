using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionLibrary
{
    public enum WallOrientation { Right, Top, Left, Bottom };

    public class Wall2 : CollisionObject
    {
        #region fields
        public WallOrientation Orientation { get; set; }        // definition of wall orientation
        #endregion

        #region constructors
        public Wall2(Vector2 coord, WallOrientation orientation)
        {
            this.M = CollisionObject.WALL_MASS;
            this.Coordinates = coord;
            if (orientation != null) this.Orientation = orientation;
            else this.Orientation = WallOrientation.Right;
        }

        public Wall2() : this(Vector2.Zero, WallOrientation.Bottom) { }
        #endregion
    }
}
