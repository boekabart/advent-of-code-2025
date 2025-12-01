namespace shared;

public static class ParsingExtensions
{
    public static IEnumerable<string> TrimmedLines(this string multiLineString) =>
        multiLineString.Split(new[] { '\n' })
            .Select(s => s.Trim());

    public static IEnumerable<string> Lines(this string multiLineString) =>
        multiLineString
            .Replace("\r\n", "\n")
            .Split(new[] { '\n' });

    public static IEnumerable<string> NotEmptyLines(this string multiLineString) =>
        multiLineString
            .Lines()
            .Where(s => s.Length > 0);

    public static IEnumerable<string> NotEmptyTrimmedLines(this string multiLineString) =>
        multiLineString
            .TrimmedLines()
            .Where(s => s.Length > 0);

    public static int? AsIntOrNull(this string txt) => int.TryParse(txt, out var number) ? number : null;
    public static int AsInt(this string txt) => int.Parse(txt);

    public static IEnumerable<int?> AsIntsOrNulls(this IEnumerable<string> lines) => lines.Select(AsIntOrNull);
}
