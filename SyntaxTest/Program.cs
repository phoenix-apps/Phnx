using MarkSFrancis.Console;
using MarkSFrancis.Drawing;
using MarkSFrancis.Drawing.Shapes;

namespace SyntaxTest
{
    class Program
    {
        public static readonly ConsoleIo Console = new ConsoleIo();

        static void Main()
        {
            Polygon p = new Square(new Point(0, 0), 5);

            Console.WriteLine(p.Area);

            Console.ReadKey();
        }
    }
}
