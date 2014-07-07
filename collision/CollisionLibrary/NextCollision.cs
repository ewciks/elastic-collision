using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollisionLibrary
{
    /// <summary>
    /// Object that stores data about next collision between two object: time and objects. 
    /// </summary>
    public class NextCollision
    {
        #region fields
        /// <summary>
        /// Positive value of collision time;
        /// </summary>
        public float Time { set; get; }
        /// <summary>
        /// Fisrt of collision objects. In ball-wall collision it's a ball.
        /// </summary>
        public CollisionObject Obj1 { set; get; }
        /// <summary>
        /// Second of collision objects. In ball-wall collision it's a wall.
        /// </summary>
        public CollisionObject Obj2 { set; get; }
        #endregion fields

        #region constructors
        public NextCollision(float time, CollisionObject obj1, CollisionObject obj2)
        {
            if (time >= 0) this.Time = time;
            else this.Time = 0.0f;
            this.Obj1 = obj1;
            this.Obj2 = obj2;
        }
        #endregion constructors

        #region overrides
        public override string ToString()
        {
            if (Obj1.M < Wall2.WALL_MASS && Obj2.M >= Wall2.WALL_MASS)
            {
                Wall2 w = (Wall2)(Obj2);
                return "ball [id=" + Obj1.Id + "] - wall [" + w.Orientation.ToString() + "]";
            }
            else
            {
                return "ball [id=" + Obj1.Id + "] - ball [id=" + Obj2.Id + "]";
            }
        }
        #endregion
    }
}
