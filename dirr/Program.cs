using System;
using System.IO;
using System.Linq;

namespace dirr
{
    class Program
    {
        static readonly int _paddingLeft = 3;
        static readonly int _nameMaxLength = 128;
        static readonly int _sizeMaxLength = 16;

        static void Main(string[] args)
        {
            try
            {
                var path = ParsePath(args) ?? Environment.CurrentDirectory;
                var dir = new DirectoryInfo(path);
                var subdirs = dir.EnumerateDirectories().ToArray();
                var files = dir.EnumerateFiles().ToArray();

                DisplayContent(subdirs, files);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DisplayContent(DirectoryInfo[] subdirs, FileInfo[] files)
        {
            DisplayContent(subdirs);
            DisplayContent(files);
        }

        private static void DisplayContent(DirectoryInfo[] subdirs)
        {
            foreach(var subdir in subdirs)
            {
                Write(new string(' ', _paddingLeft)); 
                Write(subdir.Name.PadRight(_nameMaxLength, ' '), ConsoleColor.DarkYellow);
                Write(Environment.NewLine);
            }
        }

        private static void DisplayContent(FileInfo[] files)
        {
            foreach (var file in files)
            {
                Write(new string(' ', _paddingLeft)); 
                Write(file.Name.PadRight(_nameMaxLength, ' '), ConsoleColor.Cyan);
                Write(ParseSize(file.Length).PadLeft(_sizeMaxLength, ' '), ConsoleColor.Cyan);
                Write(Environment.NewLine);
            }
        }

        private static string ParsePath(string[] args)
        {
            if (args.Length == 0) return null;
            if (args.Length > 1)
            {
                Console.WriteLine("Too many arguments !! 😮");
                Environment.Exit(1);
            }

            var dir = new DirectoryInfo(args[0]);
            if (!dir.Exists)
            {
                Console.WriteLine("Cannot find the directory! 🔍");
                Environment.Exit(1);
            }

            return dir.FullName;
        }

        private static string ParseSize(long size)
        {
            var sizeInB = size / 1;
            var sizeInKB = size / 1_000;
            var sizeInMB = size / 1_000_000;
            var sizeInGB = size / 1_000_000_000;
            var sizeInTB = size / 1_000_000_000_000;

            if (sizeInB > 0 && sizeInB < 1000)
                return "B";
            if (sizeInB > 0 && sizeInKB < 1000)
                return "kB";
            if (sizeInB > 0 && sizeInMB < 1000)
                return "MB";
            if (sizeInB > 0 && sizeInGB < 1000)
                return "GB";
            if (sizeInB > 0 && sizeInTB < 1000)
                return "TB";
            else
                return "";

            return "";
        }

        private static void WriteLine(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        private static void Write(string str, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ResetColor();
        }
    }
}
