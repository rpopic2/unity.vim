
var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

var p = Path.Combine(home, "Library", "Logs", "Unity", "Editor.log");

Console.WriteLine(p);

using var logFile = File.OpenText(p);

string? SkipUntil(string s) {
    var prevLine = string.Empty;
    while (true) {
        var line = logFile.ReadLine();
        if (line == null)
            break;

        if (line.StartsWith(s)) {
            return prevLine;
        }
        prevLine = line;
    }
    return null;
}

IEnumerable<(string, int)> PrintUntilBlankLine() {
    while (true) {
        var line = logFile?.ReadLine();
        if (line == null || line.Length == 0)
            break;

        var begin = line.IndexOf("(at ");
        var end = line.LastIndexOf(":");

        if (begin < 0 || end < 0)
            continue;
        var err_source = line[(begin + 4)..end];

        var num_begin = end + 1;
        var parenEnd = line.LastIndexOf(")");
        if (num_begin < 0 || parenEnd < 0)
            continue;
        var err_line = line[num_begin..parenEnd];


        yield return (err_source, int.Parse(err_line));

        if (line.StartsWith('(')) {
            Console.ReadLine();
            break;
        }
    }
}


SkipUntil("[LAYOUT]");
Console.WriteLine();

var errmsg = SkipUntil("UnityEngine.StackTraceUtility:ExtractStackTrace ()");
if (errmsg is not null) {
    Console.WriteLine(errmsg);
}

var iter = PrintUntilBlankLine();

foreach (var (k, v) in iter) {
    Console.WriteLine(k);
    Console.WriteLine(v);
}


