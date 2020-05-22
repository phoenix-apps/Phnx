using System;
using System.Drawing;

namespace Phnx.Drawing
{
    /// <summary>
    /// A double precision point
    /// </summary>
    public struct PointD
    {
        /// <summary>
        /// Create a new <see cref="PointD"/>
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The X coordinate
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// The Y coordinate
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Translates a <see cref='PointD'/> by a given <see cref='Size'/>
        /// </summary>
        public static PointD operator +(PointD pt, Size sz) => Add(pt, sz);

        /// <summary>
        /// Translates a <see cref='PointD'/> by the negative of a given <see cref='Size'/>
        /// </summary>
        public static PointD operator -(PointD pt, Size sz) => Subtract(pt, sz);

        /// <summary>
        /// Translates a <see cref='PointD'/> by a given <see cref='SizeD'/>
        /// </summary>
        public static PointD operator +(PointD pt, SizeD sz) => Add(pt, sz);

        /// <summary>
        /// Translates a <see cref='PointD'/> by the negative of a given <see cref='SizeD'/>
        /// </summary>
        public static PointD operator -(PointD pt, SizeD sz) => Subtract(pt, sz);

        /// <summary>
        /// Compares two <see cref='PointD'/> objects. The result specifies whether the values of the <see cref='X'/> and <see cref='Y'/> properties of the two <see cref='PointD'/> objects are equal
        /// </summary>
        public static bool operator ==(PointD left, PointD right) => left.X == right.X && left.Y == right.Y;

        /// <summary>
        /// Compares two <see cref='PointD'/> objects. The result specifies whether the values of the <see cref='X'/> or <see cref='Y'/> properties of the two <see cref='PointD'/>s are unequal
        /// </summary>
        public static bool operator !=(PointD left, PointD right) => !(left == right);

        /// <summary>
        /// Translates a <see cref='PointD'/> by a given <see cref='Size'/>
        /// </summary>
        public static PointD Add(PointD pt, Size sz) => new PointD(pt.X + sz.Width, pt.Y + sz.Height);

        /// <summary>
        /// Translates a <see cref='PointD'/> by a given <see cref='SizeD'/>
        /// </summary>
        public static PointD Add(PointD pt, SizeD sz) => new PointD(pt.X + sz.Width, pt.Y + sz.Height);

        /// <summary>
        /// Translates a <see cref='PointD'/> by the negative of a given <see cref='Size'/>
        /// </summary>
        public static PointD Subtract(PointD pt, Size sz) => new PointD(pt.X - sz.Width, pt.Y - sz.Height);

        /// <summary>
        /// Translates a <see cref='PointD'/> by the negative of a given <see cref='SizeD'/>
        /// </summary>
        public static PointD Subtract(PointD pt, SizeD sz) => new PointD(pt.X - sz.Width, pt.Y - sz.Height);

        /// <summary>
        /// Converts a <see cref="PointF"/> to a <see cref="PointD"/>
        /// </summary>
        /// <param name="coordinatesToConvert">The coordinates to convert</param>
        public static implicit operator PointD(PointF coordinatesToConvert)
        {
            return new PointD(coordinatesToConvert.X, coordinatesToConvert.Y);
        }

        /// <summary>
        /// Converts a <see cref="Point"/> to a <see cref="PointD"/>
        /// </summary>
        /// <param name="coordinatesToConvert">The coordinates to convert</param>
        public static implicit operator PointD(Point coordinatesToConvert)
        {
            return new PointD(coordinatesToConvert.X, coordinatesToConvert.Y);
        }

        /// <summary>
        /// Get whether the <paramref name="obj"/> is a <see cref="PointD"/> and is the same as this <see cref="PointD"/>
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>Whether the <paramref name="obj"/> is a <see cref="PointD"/> and is the same as this <see cref="PointD"/></returns>
        public override bool Equals(object obj)
        {
            if (obj is PointD p)
            {
                return Equals(p);
            }

            return false;
        }

        /// <summary>
        /// Get whether the <paramref name="other"/> is the same as this <see cref="PointD"/>
        /// </summary>
        /// <param name="other">The other <see cref="PointD"/> to compare</param>
        /// <returns>Whether the <paramref name="other"/> is the same as this <see cref="PointD"/></returns>
        public bool Equals(PointD other) => this == other;

        /// <summary>
        /// Convert to a <see cref="string"/>
        /// </summary>
        /// <returns>This <see cref="PointD"/> converted to a <see cref="string"/></returns>
        public override string ToString() => "{X=" + X.ToString() + ", Y=" + Y.ToString() + "}";

        /// <summary>
        /// Get the unique hash code
        /// </summary>
        /// <returns>The unique hash code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <summary>
        /// A default, empty <see cref="PointD"/>
        /// </summary>
        public static readonly PointD Empty = new PointD();
    }
}
