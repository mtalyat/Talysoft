using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talysoft.IO;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// A pair of floats.
    /// </summary>
    public struct float2 : ISaveable, IMathematical<float2>
    {
        public float X { get; set; }
        public float Y { get; set; }

        public float2(float x, float y)
        {
            X = x;
            Y = y;
        }

        #region Math

        public float Magnitude()
        {
            return (float)System.Math.Sqrt(MagnitudeSquared());
        }

        public float MagnitudeSquared()
        {
            return X * X + Y * Y;
        }

        #region Operators

        public static float2 operator +(float2 right, float2 left)
        {
            return new float2(right.X + left.X, right.Y + left.Y);
        }

        public static float2 operator -(float2 right, float2 left)
        {
            return new float2(right.X - left.X, right.Y - left.Y);
        }

        public static float2 operator *(float2 right, float2 left)
        {
            return new float2(right.X * left.X, right.Y * left.Y);
        }

        public static float2 operator *(float2 right, float left)
        {
            return new float2(right.X * left, right.Y * left);
        }

        public static float2 operator /(float2 right, float2 left)
        {
            return new float2(right.X / left.X, right.Y / left.Y);
        }

        public static float2 operator /(float2 right, float left)
        {
            return new float2(right.X / left, right.Y / left);
        }

        #endregion

        #endregion

        #region Serialization

        public void Serialize(BinaryWriter writer, uint version)
        {
            writer.Write(X);
            writer.Write(Y);
        }

        public void Deserialize(BinaryReader reader, uint version)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
        }

        #endregion

        #region Comparison

        public bool Equals(float2 other)
        {
            return other.X == X && other.Y == Y;
        }

        public int CompareTo(float2 other)
        {
            float m = MagnitudeSquared();
            float o = other.MagnitudeSquared();

            if (m > o) return 1;
            else if (m < o) return -1;
            else return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is float2 f && Equals(f);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        #endregion

        public override string ToString()
        {
            return $"float2{Constants.OPEN}{X}, {Y}{Constants.CLOSE}";
        }
    }
}
