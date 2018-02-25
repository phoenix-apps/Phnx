using System;
using System.Threading.Tasks;

namespace MarkSFrancis.Extensions
{
    public static class ActionExtensions
    {
        public static async Task InvokeAsync(this Action action)
        {
            await Task.Run(action);
        }
    }
}