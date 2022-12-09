using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wut7Practa
{
    public class ArrowMenu
    {
        int selected;
        int lenght;
        int offset;
        static string indent = "   ";
        List<MyFile> buttons;
        public ArrowMenu(List<MyFile> buttons, int offset = 0)
        {
            this.buttons = buttons;
            this.lenght = offset + buttons.Count - 1;
            this.offset = offset;
            this.selected = offset;
        }
        private static void RemoveCursor(int y)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(indent);
        }
        public int Select()
        {
            ShowInfo.ShowFiles(buttons, indent);
            ConsoleKey key = new ConsoleKey();
            while (key != ConsoleKey.Enter)
            {
                Console.SetCursorPosition(0, selected);
                Console.Write("->");
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Escape:
                        return -1;
                    case ConsoleKey.Enter:
                        break;
                    case ConsoleKey.UpArrow:
                        RemoveCursor(selected);
                        if (selected == offset)
                            selected = lenght;
                        else
                            selected -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        RemoveCursor(selected);
                        if (selected == lenght)
                            selected = offset;
                        else
                            selected += 1;
                        break;
                }
            }
            return selected - offset;
        }
    }

    public static class ShowInfo
    {
        public static void ShowFiles(List<MyFile> files, string offset)
        {
            string text = "";
            foreach (MyFile file in files)
            {
                if (Directory.Exists(file.Path))
                    text += offset + file.Name + "\\\n";
                else
                    text += offset + file.Name + "\n";
            }
            Console.WriteLine(text);
        }
    }
    public class MyFile
    {
        public MyFile(string name, string path)
        {
            Name = name;
            Path = path;
        }
        public string Name { get; set; }
        public string Path { get; set; }
    }
    public class FileWorking
    {
        private static List<string> GetFiles(string path)
        {
            List<string> files = new List<string>();
            foreach (string folder in Directory.GetDirectories(path))
                files.Add(folder);

            foreach (string file in Directory.GetFiles(path))
                files.Add(file);

            return files;
        }
        public static List<MyFile> LoadFolder(string path)
        {
            List<MyFile> files = new List<MyFile>();
            foreach (string file in GetFiles(path))
                files.Add(new MyFile(Path.GetFileName(file), file));
            return files;
        }
    }
}