using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollisionLibrary
{
    /// <summary>
    /// Class for calculating the smallest positive solution of quadratic equation.
    /// </summary>
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
        /// <summary>
        /// Constructor with parameters for quadratic equation. For null substitutes with zero values.
        /// </summary>
        /// <param name="a">Parameter for second power argument.</param>
        /// <param name="b">Parameter for first power argument.</param>
        /// <param name="c">Parameter for zero power argument.</param>
        public QuadraticEquation(float a, float b, float c)
        {
            if (a != null) this.a = a; else this.a = 0.0f;
            if (b != null) this.b = b; else this.b = 0.0f;
            if (c != null) this.c = c; else this.c = 0.0f;
        }

        /// <summary>
        /// Default constructor with zero values.
        /// </summary>
        public QuadraticEquation() : this(0.0f, 0.0f, 0.0f) { }
        #endregion constructors

        #region operations
        /// <summary>
        /// Calculates delta value for quadratic equation.
        /// </summary>
        private void CalcDelta()
        {
            float val = b * b - 4 * a * c;
            if (val * 7.0f < 0.0f) val = 0.0f;
            delta = (float)Math.Sqrt(val);
        }

        /// <summary>
        /// Returns smaller positive solution for quadratic solution. If such value doesn't exist, returns -1.0f;
        /// </summary>
        /// <returns>Smaller positive solution for quadratic solution or -1.0f.</returns>
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
        #endregion operations
    }
}
