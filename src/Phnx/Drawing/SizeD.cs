using System;
using System.Drawing;

namespace Phnx.Drawing
{
    /// <summary>
    /// A double precision size
    /// </summary>
    public struct SizeD : IEquatable<SizeD>
    {
        /// <summary>
        /// A default, empty <see cref="SizeD"/>
        /// </summary>
        public static readonly SizeD Empty = new SizeD();

        /// <summary>
        /// Initializes a new instance of the <see cref='SizeD'/> class from the specified existing <see cref='SizeD'/>System.Drawing.SizeD
        /// </summary>
        public SizeD(SizeD size)
        {
            Width = size.Width;
            Height = size.Height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref='SizeD'/> class from the specified <see cref='PointD'/>
        /// </summary>
        public SizeD(PointD pt)
        {
            Width = pt.X;
            Height = pt.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref='SizeD'/> class from the specified dimensions
        /// </summary>
        public SizeD(double width, double height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Performs vector addition of two <see cref='SizeD'/>s
        /// </summary>
        public static SizeD operator +(SizeD sz1, SizeD sz2) => Add(sz1, sz2);

        /// <summary>
        /// Contracts a <see cref='SizeD'/> by another <see cref='SizeD'/>
        /// </summary>        
        public static SizeD operator -(SizeD sz1, SizeD sz2) => Subtract(sz1, sz2);

        /// <summary>
        /// Multiplies <see cref="SizeD"/> by a <see cref="double"/> producing <see cref="SizeD"/>
        /// </summary>
        /// <param name="left">Multiplier of type <see cref="double"/></param>
        /// <param name="right">Multiplicand of type <see cref="SizeD"/></param>
        /// <returns>Product of type <see cref="SizeD"/>.</returns>
        public static SizeD operator *(double left, SizeD right) => Multiply(right, left);

        /// <summary>
        /// Multiplies <see cref="SizeD"/> by a <see cref="double"/> producing <see cref="SizeD"/>
        /// </summary>
        /// <param name="left">Multiplicand of type <see cref="SizeD"/></param>
        /// <param name="right">Multiplier of type <see cref="double"/></param>
        /// <returns>Product of type <see cref="SizeD"/>.</returns>
        public static SizeD operator *(SizeD left, double right) => Multiply(left, right);

        /// <summary>
        /// Divides <see cref="SizeD"/> by a <see cref="double"/> producing <see cref="SizeD"/>
        /// </summary>
        /// <param name="left">Dividend of type <see cref="SizeD"/></param>
        /// <param name="right">Divisor of type <see cref="int"/></param>
        /// <returns>Result of type <see cref="SizeD"/></returns>
        public static SizeD operator /(SizeD left, double right)
            => new SizeD(left.Width / right, left.Height / right);

        /// <summary>
        /// Tests whether two <see cref='SizeD'/>s are identical
        /// </summary>
        public static bool operator ==(SizeD sz1, SizeD sz2) => sz1.Width == sz2.Width && sz1.Height == sz2.Height;

        /// <summary>
        /// Tests whether two <see cref='SizeD'/>s are different
        /// </summary>
        public static bool operator !=(SizeD sz1, SizeD sz2) => !(sz1 == sz2);

        /// <summary>
        /// Converts the specified <see cref='SizeD'/> to a <see cref='PointD'/>
        /// </summary>
        public static explicit operator PointD(SizeD size) => new PointD(size.Width, size.Height);

        /// <summary>
        /// Represents the horizontal component of this <see cref='SizeD'/>
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Represents the vertical component of this <see cref='SizeD'/>
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Performs vector addition of two <see cref='SizeD'/> objects
        /// </summary>
        public static SizeD Add(SizeD sz1, SizeD sz2) => new SizeD(sz1.Width + sz2.Width, sz1.Height + sz2.Height);

        /// <summary>
        /// Contracts a <see cref='SizeD'/> by another <see cref='SizeD'/>
        /// </summary>        
        public static SizeD Subtract(SizeD sz1, SizeD sz2) => new SizeD(sz1.Width - sz2.Width, sz1.Height - sz2.Height);

        /// <summary>
        /// Converts a <see cref="SizeF"/> to a <see cref="SizeD"/>
        /// </summary>
        /// <param name="sizeToConvert">The size to convert</param>
        public static implicit operator SizeD(SizeF sizeToConvert)
        {
            return new SizeD(sizeToConvert.Width, sizeToConvert.Height);
        }

        /// <summary>
        /// Converts a <see cref="Size"/> to a <see cref="SizeD"/>
        /// </summary>
        /// <param name="sizeToConvert">The size to convert</param>
        public static implicit operator SizeD(Size sizeToConvert)
        {
            return new SizeD(sizeToConvert.Width, sizeToConvert.Height);
        }

        /// <summary>
        /// Get whether the <paramref name="obj"/> is a <see cref="SizeD"/> and is the same as this <see cref="SizeD"/>
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>Whether the <paramref name="obj"/> is a <see cref="SizeD"/> and is the same as this <see cref="SizeD"/></returns>
        public override bool Equals(object obj)
        {
            if (obj is SizeD s)
            {
                return Equals(s);
            }

            return false;
        }

        /// <summary>
        /// Get whether the <paramref name="other"/> is the same as this <see cref="SizeD"/>
        /// </summary>
        /// <param name="other">The other <see cref="SizeD"/> to compare</param>
        /// <returns>Whether the <paramref name="other"/> is the same as this <see cref="SizeD"/></returns>
        public bool Equals(SizeD other) => this == other;

        /// <summary>
        /// Get the unique hash code
        /// </summary>
        /// <returns>The unique hash code</returns>
        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Converts to a <see cref="PointD"/>
        /// </summary>
        /// <returns>A <see cref="PointD"/> with an <see cref="PointD.X"/> of <see cref="Width"/>, and <see cref="PointD.Y"/> of <see cref="Height"/></returns>
        public PointD ToPointD() => (PointD)this;

        /// <summary>
        /// Converts to a <see cref="Size"/>, by truncating the <see cref="Width"/> and <see cref="Height"/>
        /// </summary>
        /// <returns>A <see cref="Size"/> containing the values of this <see cref="SizeD"/> truncated</returns>
        public Size ToSize() => new Size((int)Math.Truncate(Width), (int)Math.Truncate(Height));

        /// <summary>
        /// Convert to a <see cref="string"/>
        /// </summary>
        /// <returns>This <see cref="PointD"/> converted to a <see cref="string"/></returns>
        public override string ToString() => "{Width=" + Width.ToString() + ", Height=" + Height.ToString() + "}";

        /// <summary>
        /// Multiplies <see cref="SizeD"/> by a <see cref="double"/> producing <see cref="SizeD"/>.
        /// </summary>
        /// <param name="size">Multiplicand of type <see cref="SizeD"/>.</param>
        /// <param name="multiplier">Multiplier of type <see cref="double"/>.</param>
        /// <returns>Product of type SizeD.</returns>
        private static SizeD Multiply(SizeD size, double multiplier) =>
            new SizeD(size.Width * multiplier, size.Height * multiplier);
    }
}
