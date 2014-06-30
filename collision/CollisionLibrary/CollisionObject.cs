using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CollisionLibrary
{
    public class CollisionObject
    {
        private static int instances = 0;
        public int Id { get; set; }
        public Vector2 Coordinates { get; set; }                // cooridantes
        
        public CollisionObject()
        {
            instances++;
            this.Id = instances;
        }

        ~CollisionObject()
        {
            instances--;
        }

        public static int GetActiveInstances()
        {
            return instances;
        }
    }
}
