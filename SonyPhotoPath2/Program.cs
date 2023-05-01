using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

string[] files = Directory.GetFiles(@"E:\DCIM", "*.jpg", SearchOption.AllDirectories);
var folderToMoveBase = Environment.GetEnvironmentVariable("Sony");
Console.WriteLine(folderToMoveBase);

foreach (string file in files)
{
    Console.WriteLine(file);

    using var fs = new FileStream(file, FileMode.Open, FileAccess.Read);
    var img = Image.FromStream(fs, false, false);
    var propItem = img.GetPropertyItem(36867);

    var dateTaken = new DateTime(
    int.Parse(Encoding.UTF8.GetString(propItem.Value).Substring(0, 4)),
    int.Parse(Encoding.UTF8.GetString(propItem.Value).Substring(5, 2)),
    int.Parse(Encoding.UTF8.GetString(propItem.Value).Substring(8, 2)),
    int.Parse(Encoding.UTF8.GetString(propItem.Value).Substring(11, 2)),
    int.Parse(Encoding.UTF8.GetString(propItem.Value).Substring(14, 2)),
    int.Parse(Encoding.UTF8.GetString(propItem.Value).Substring(17, 2)));

    var newPath = $@"{folderToMoveBase}\{dateTaken.ToString("yyyyMMdd")}";
    if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

    var fileInfo = new FileInfo(file);
    if (File.Exists(Path.Combine(newPath, fileInfo.Name))) continue;

    fileInfo.CopyTo(Path.Combine(newPath, fileInfo.Name), false);
}

