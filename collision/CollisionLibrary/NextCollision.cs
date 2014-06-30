using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollisionLibrary
{
    public class NextCollision
    {
        public float Time { set; get; }
        public CollisionObject Obj1 { set; get; }
        public CollisionObject Obj2 { set; get; }

        public NextCollision(float time, CollisionObject obj1, CollisionObject obj2)
        {
            this.Time = time;
            this.Obj1 = obj1;
            this.Obj2 = obj2;
        }
    }
}
