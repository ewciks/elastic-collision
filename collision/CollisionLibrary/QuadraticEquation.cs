using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollisionLibrary
{
    public class QuadraticEquation
    {
        #region fields
        private float a;
        private float b;
        private float c;
        private float delta;
        private float x1;
        private float x2;
        #endregion

        #region constructors
        public QuadraticEquation(float a, float b, float c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuadraticEquation() : this(0.0f, 0.0f, 0.0f) { }
        #endregion

        private void CalcDelta()
        {
            delta = (float)Math.Sqrt(b * b - 4 * a * c);
        }

        /// <summary>
        /// Returns smaller positive x value
        /// If value doesn't exist, returns -1.0f;
        /// </summary>
        /// <returns></returns>
        public float CalcSmallerPositiveX()
        {
            CalcDelta();
            if (delta >= 0.0f)
            {
                x1 = (-b - delta) / (2 * a);
                x2 = (-b + delta) / (2 * a);
                if (x1 >= 0.0f && x1 <= x2) return x1;
                else if (x2 >= 0.0f && x2 <= x1) return x2;
                else return -1.0f;
            }
            else return -1.0f;
        }
    }
}
