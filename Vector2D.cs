using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talysoft.IO;

namespace Talysoft
{
    public struct Vector2D : ISaveable
    {
        public static Vector2D Up => new Vector2D(0, 1);
        public static Vector2D Down => new Vector2D(0, -1);
        public static Vector2D Left => new Vector2D(-1, 0);
        public static Vector2D Right => new Vector2D(1, 0);
        public static Vector2D Max => new Vector2D(float.MaxValue, float.MaxValue);
        public static Vector2D Min => new Vector2D(float.MinValue, float.MinValue);

        public float X { get; set; }
        public float Width => X;

        public float Y { get; set; }
        public float Height => Y;

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Vector2D(System.Drawing.PointF v) => new Vector2D(v.X, v.Y);
        public static implicit operator System.Drawing.PointF(Vector2D v) => new System.Drawing.PointF(v.X, v.Y);

        public static implicit operator Vector2D(System.Drawing.SizeF v) => new Vector2D(v.Width, v.Height);
        public static implicit operator System.Drawing.SizeF(Vector2D v) => new System.Drawing.SizeF(v.X, v.Y);

        public static Vector2D operator +(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2D operator -(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2D operator *(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X * right.X, left.Y * right.Y);
        }

        public static Vector2D operator *(Vector2D left, float right)
        {
            return new Vector2D(left.X * right, left.Y * right);
        }

        public static Vector2D operator /(Vector2D left, Vector2D right)
        {
            return new Vector2D(left.X / right.X, left.Y / right.Y);
        }

        public static Vector2D operator /(Vector2D left, float right)
        {
            return new Vector2D(left.X / right, left.Y / right);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

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
    }
}
