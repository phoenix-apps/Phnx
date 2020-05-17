using System.IO;

namespace Phnx.Console.Tests.TestHelpers
{
    public static class Factory
    {
        public static TextIoHelper TextIo(out StreamReader fromTextIo, out StreamWriter toTextIo)
        {
            var textIoInput = new PipeStream(false);
            var textIoOutput = new PipeStream();

            fromTextIo = textIoOutput.Tail;
            toTextIo = textIoInput.Head;

            return new TextIoHelper(textIoInput.Tail, textIoOutput.Head, true);
        }
    }
}
