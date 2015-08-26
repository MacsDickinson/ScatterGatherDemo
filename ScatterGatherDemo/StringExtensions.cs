namespace ScatterGatherDemo
{
    public static class StringExtensions
    {
        public static string SubstringBetweenStrings(this string value, string start, string end, int startPos)
        {
            var returnStartPos = value.IndexOf(start, startPos) + start.Length;
            var returnEndPos = value.IndexOf(end, returnStartPos);
            return value.Substring(returnStartPos, returnEndPos - returnStartPos);
        }
    }
}