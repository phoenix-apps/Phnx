using NUnit.Framework;
using Phnx.Drawing.Shapes;

namespace Phnx.Drawing.Tests.Shapes
{
    public class PolygonTests
    {
        [Test]
        public void NewPolygon_PopulatesVertices()
        {
            var points = new PointD[]
            {
                new PointD(0, 5),
                new PointD(5, 5),
                new PointD(5, 0),
                new PointD(0, 0),
            };

            var polygon = new Polygon(points);

            CollectionAssert.AreEqual(points, polygon.Vertices);
        }

        [Test]
        public void NewPolygon_WithNullVertices_SetsVerticesToNull()
        {
            PointD[] points = null;

            var polygon = new Polygon(points);

            Assert.IsNull(polygon.Vertices);
        }

        [Test]
        public void GetSides_WhenTwoVerticesGiven_GetsTwoSides()
        {
            PointD[] points = new PointD[]
            {
                new PointD(0, 0),
                new PointD(5, 0)
            };

            Side[] expected = new Side[]
            {
                new Side(points[0], points[1]),
                new Side(points[1], points[0])
            };

            var polygon = new Polygon(points);

            CollectionAssert.AreEqual(expected, polygon.Sides);
        }

        [Test]
        public void GetSides_WhenVerticesAreNull_GetsNull()
        {
            var polygon = new Polygon();

            Assert.IsNull(polygon.Sides);
        }

        [Test]
        public void GetSides_WhenVerticesAreEmpty_GetsEmptyArray()
        {
            var polygon = new Polygon(new PointD[0]);

            CollectionAssert.IsEmpty(polygon.Sides);
        }

        [Test]
        public void GetSides_When1Vertex_GetsEmptyArray()
        {
            var polygon = new Polygon(new PointD(0, 0));

            CollectionAssert.IsEmpty(polygon.Sides);
        }

        [Test]
        public void GetSides_WhenNullVertices_GetsZero()
        {
            var polygon = new Polygon();

            Assert.AreEqual(0, polygon.TotalLength);
        }

        [Test]
        public void GetTotalLength_When1Vertex_GetsZero()
        {
            var polygon = new Polygon(new PointD(0, 0));

            Assert.AreEqual(0, polygon.TotalLength);
        }

        [Test]
        public void GetSides_When4Vertices_GetsTotalLength()
        {
            var points = new PointD[]
            {
                new PointD(0, 5),
                new PointD(5, 5),
                new PointD(5, 0),
                new PointD(0, 0),
            };

            var polygon = new Polygon(points);

            Assert.AreEqual(20, polygon.TotalLength);
        }

        [Test]
        public void GetSidesCount_When4Vertices_Gets4()
        {
            var points = new PointD[]
            {
                new PointD(0, 5),
                new PointD(5, 5),
                new PointD(5, 0),
                new PointD(0, 0),
            };

            var polygon = new Polygon(points);

            Assert.AreEqual(4, polygon.SidesCount);
        }

        [Test]
        public void GetSidesCount_When1Vertex_Gets0()
        {
            var polygon = new Polygon(new PointD(0, 0));

            Assert.AreEqual(0, polygon.SidesCount);
        }

        [Test]
        public void GetSidesCount_WhenVerticesIsNull_Gets0()
        {
            var polygon = new Polygon();

            Assert.AreEqual(0, polygon.SidesCount);
        }

        [Test]
        public void GetArea_WhenVerticesIsNull_Gets0()
        {
            var polygon = new Polygon();

            Assert.AreEqual(0, polygon.Area);
        }

        [Test]
        public void GetArea_When2Vertices_Gets0()
        {
            var polygon = new Polygon(new PointD(0, 0), new PointD(5, 5));

            Assert.AreEqual(0, polygon.Area);
        }

        [Test]
        public void GetArea_When3Vertices_GetsArea()
        {
            var expected = 6;

            var polygon = new Polygon(new PointD(0, 0), new PointD(4, 0), new PointD(0, 3));

            Assert.AreEqual(expected, polygon.Area);
        }

        [Test]
        public void GetArea_When4Vertices_GetsArea()
        {
            var expected = 12;

            var polygon = new Polygon(new PointD(0, 0), new PointD(4, 0), new PointD(4, 3), new PointD(0, 3));

            Assert.AreEqual(expected, polygon.Area);
        }
    }
}
