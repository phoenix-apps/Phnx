namespace MarkSFrancis.DotNetExtensions
{
    public static class BooleanExtensions
    {
        public static string ToString(this bool condition, bool upperCase)
        {
            string conditionAsString = condition.ToString();

            return upperCase ? conditionAsString.ToUpper() : conditionAsString.ToLower();
        }

        public static string ToString(this bool condition, string trueText, string falseText)
        {
            return condition ? trueText : falseText;
        }
    }
}