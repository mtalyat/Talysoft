using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talysoft.IO;
using System.IO;

namespace Talysoft.Mathematics
{
    /// <summary>
    /// A pair of decimals.
    /// </summary>
    public struct decimal2 : ISaveable, IMathematical<decimal2>
    {
        public float X { get; set; }
        public float Y { get; set; }

        public decimal2(float x, float y)
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

        public static decimal2 operator +(decimal2 right, decimal2 left)
        {
            return new decimal2(right.X + left.X, right.Y + left.Y);
        }

        public static decimal2 operator -(decimal2 right, decimal2 left)
        {
            return new decimal2(right.X - left.X, right.Y - left.Y);
        }

        public static decimal2 operator *(decimal2 right, decimal2 left)
        {
            return new decimal2(right.X * left.X, right.Y * left.Y);
        }

        public static decimal2 operator *(decimal2 right, float left)
        {
            return new decimal2(right.X * left, right.Y * left);
        }

        public static decimal2 operator /(decimal2 right, decimal2 left)
        {
            return new decimal2(right.X / left.X, right.Y / left.Y);
        }

        public static decimal2 operator /(decimal2 right, float left)
        {
            return new decimal2(right.X / left, right.Y / left);
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

        public bool Equals(decimal2 other)
        {
            return other.X == X && other.Y == Y;
        }

        public int CompareTo(decimal2 other)
        {
            float m = MagnitudeSquared();
            float o = other.MagnitudeSquared();

            if (m > o) return 1;
            else if (m < o) return -1;
            else return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is decimal2 f && Equals(f);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        #endregion

        public override string ToString()
        {
            return $"decimal2{Constants.OPEN}{X}, {Y}{Constants.CLOSE}";
        }
    }
}
