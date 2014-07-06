using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Class of vectors transformations need in collision simulation.
    /// </summary>
    public static class Transformation
    {
        /// <summary>
        /// Calculates unit normal vector to circle (ball) b1 in point of contact
        /// </summary>
        /// <param name="b1">First ball in collision.</param>
        /// <param name="b2">Second ball in collision.</param>
        /// <returns></returns>
        public static Vector2 CalcNormal(Ball2 b1, Ball2 b2)
        {
            Vector2 normal = Vector2.Zero;
            float xDiff = b2.Coordinates.X - b1.Coordinates.X;
            float yDiff = b2.Coordinates.Y - b1.Coordinates.Y;
            float normalLength = (float)Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
            normal.X = xDiff / normalLength;
            normal.Y = yDiff / normalLength;
            
            return normal;
        }

        /// <summary>
        /// Calculates tangent vector.
        /// </summary>
        /// <param name="b1">First ball in collision.</param>
        /// <param name="b2">Second ball in collision.</param>
        /// <returns></returns>
        public static Vector2 CalcTangent(Ball2 b1, Ball2 b2)
        {
            Vector2 tangent = Vector2.Zero;
            float xDiff = -b2.Coordinates.Y + b1.Coordinates.Y;
            float yDiff = b2.Coordinates.X - b1.Coordinates.X;
            float tangentLength = (float)Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
            tangent.X = xDiff / tangentLength;
            tangent.Y = yDiff / tangentLength;

            return tangent;
        }

        /// <summary>
        /// Calculates tangent vector having already calculated normal vector.
        /// </summary>
        /// <param name="normal">Normal vector.</param>
        /// <returns></returns>
        public static Vector2 CalcTangent(Vector2 normal)
        {
            return new Vector2(-normal.Y, normal.X); ;
        }

        /// <summary>
        /// Projection of vector v to axis descriped by vector 'axis'. As a fact it's just a dot product.
        /// </summary>
        /// <param name="v">Projected vector.</param>
        /// <param name="axis">Axis on which vector will be projected.</param>
        /// <returns></returns>
        public static float Projection(Vector2 v, Vector2 axis)
        {
            return Vector2.Dot(v, axis);
        }
    }
}
