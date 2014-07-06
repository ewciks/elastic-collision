using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionLibrary
{
    /// <summary>
    /// Common part of attributes of collision objects with unique ID for each object.
    /// </summary>
    public class CollisionObject
    {
        #region fields
        /// <summary>
        /// Default mass for wall which should represent positive infinity.
        /// </summary>
        public const float WALL_MASS = 10000000.0f;
        /// <summary>
        /// Class instances counter.
        /// </summary>
        private static int instances = 0;
        /// <summary>
        /// Unique id for created object.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Two dimensional vecotr of coordinates in [m].
        /// </summary>
        public Vector2 Coordinates { get; set; }
        /// <summary>
        /// Unnegative radius in [m].
        /// </summary>
        public float M { get; set; }
        #endregion fields

        #region constructors
        public CollisionObject()
        {
            instances++;
            this.Id = instances;
        }

        ~CollisionObject()
        {
            instances--;
        }
        #endregion constructors

        #region accessors
        public static int GetActiveInstances()
        {
            return instances;
        }
        #endregion accessors
    }
}
