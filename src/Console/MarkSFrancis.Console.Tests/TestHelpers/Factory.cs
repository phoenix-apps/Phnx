using System.IO;

namespace MarkSFrancis.Console.Tests.TestHelpers
{
    public static class Factory
    {
        public static TextIo TextIo(out StreamReader fromTextIo, out StreamWriter toTextIo)
        {
            var textIoInput = new TailedStream(false);
            var textIoOutput = new TailedStream();

            fromTextIo = textIoOutput.Tail;
            toTextIo = textIoInput.Head;

            return new TextIo(textIoInput.Tail, textIoOutput.Head, true);
        }
    }
}
