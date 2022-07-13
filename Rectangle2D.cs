using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talysoft.IO;

namespace Talysoft
{
    public struct Rectangle2D : ISaveable
    {
        public Vector2D Position { get; set; }
        public float X => Position.X;
        public float Y => Position.Y;

        public Vector2D Size { get; set; }
        public float Width => Size.X;
        public float Height => Size.Y;

        public Vector2D Min => Position;
        public Vector2D Max => Position + Size;
        public Vector2D Center => Position + Size / 2f;

        public Rectangle2D(Vector2D pos, Vector2D size)
        {
            Position = pos;
            Size = size;
        }

        public Rectangle2D(float x, float y, float w, float h)
        {
            Position = new Vector2D(x, y);
            Size = new Vector2D(w, h);
        }

        public static implicit operator Rectangle2D(System.Drawing.RectangleF rect) => new Rectangle2D(rect.X, rect.Y, rect.Width, rect.Height);
        public static implicit operator System.Drawing.RectangleF(Rectangle2D rect) => new System.Drawing.RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

        public void Serialize(BinaryWriter writer, uint version)
        {
            Position.Serialize(writer, version);

            Size.Serialize(writer, version);
        }

        public void Deserialize(BinaryReader reader, uint version)
        {
            Position.Deserialize(reader, version);

            Size.Deserialize(reader, version);
        }
    }
}
