using System;

Console.WriteLine("=== Directory.GetCurrentDirectory() ===");
string cwd = Directory.GetCurrentDirectory();
Console.WriteLine(cwd);

Console.WriteLine("\n=== AppContext.BaseDirectory（程序集所在目录）===");
string baseDir = AppContext.BaseDirectory;
Console.WriteLine(baseDir);

Console.WriteLine("\n按回车退出");
Console.ReadLine();