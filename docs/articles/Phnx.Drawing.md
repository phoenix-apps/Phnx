# Phnx.Drawing

This library contains tools to help you draw, including 2D geometry helpers, color and hex code helpers, co-ordinates and some basic 2D mathematics

# Quick Examples

For a full list of all things possible with Phnx.Drawing, please check the [API reference guide](https://phoenix-apps.github.io/Phnx-Wiki/api/Phnx.Drawing.html)

## Color Extensions

```cs
using Phnx.Drawing;

Color originalColor = Color.Red;
Hex hex = originalColor.ToHex(false);

// Output: FF0000
Console.WriteLine(hex.HexString);

// Or
string hexString = HexColorConverter.GetHexCode(originalColor);

// Output: FF0000
Console.WriteLine(hexString);

Color hexAsColor = HexColorConverter.GetColor(hexString);

// Output: Color [Red]
Console.WriteLine(hexString);
```

## Double Extensions

```cs
using Phnx.Drawing;

double original = 1.5 * Math.PI;
double degrees = original.ToDegrees();

// Output: 270
Console.WriteLine(degrees);

double radians = degrees.ToRadians();

// Output: 4.7123889803846897
Console.WriteLine(radians);
```

## Coordinate Geometry

```cs
PointD point = new PointD(7.5, -12);

// Output: {X=7.5, Y=-12}
Console.WriteLine(point);

SizeD size = new SizeD(7.5, -12);

// Output: {Width=7.5, Height=-12}
Console.WriteLine(point);

/* Represents triangle

  /|
 / |
/  |
---|
*/

Polygon p = new Polygon(new PointD []
{
    new PointD(0, 0),
    new PointD(3, 5),
    new PointD(3, 0)
});

// Output: 7.5
Console.WriteLine(p.Area);

// Output: 13.830951894845301
Console.WriteLine(p.TotalLength);
```