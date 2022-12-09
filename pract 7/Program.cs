using System.Diagnostics;
using System.Xml.Linq;

namespace wut7Practa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string myPath = "";
            string oldPath;
            List<MyFile> myFiles = new List<MyFile>();
            while (true)
            {
                Console.Clear();
                if (myPath == "")
                {
                    Console.WriteLine("Выбор диска");
                    List<MyFile> drivesList = new List<MyFile>();
                    DriveInfo[] allDrives = DriveInfo.GetDrives();
                    foreach (DriveInfo drive in allDrives)
                    {
                        decimal AvailableFreeSpace = drive.AvailableFreeSpace / 1024 / 1024 / 1024;
                        AvailableFreeSpace = Math.Round(AvailableFreeSpace, 1);
                        MyFile myDrive = new MyFile($"{drive.Name} - Доступно {AvailableFreeSpace} гб", drive.Name);
                        drivesList.Add(myDrive);
                    }
                    ArrowMenu myFileMenu = new ArrowMenu(drivesList, 1);
                    int selected = myFileMenu.Select();
                    if (selected == -1)
                    {
                        Environment.Exit(0);
                    }
                    myPath = drivesList[selected].Path;
                }
                else
                {
                    DirectoryInfo ParentFolder = Directory.GetParent(myPath);
                    if (ParentFolder == null)
                        oldPath = "";
                    else
                        oldPath = ParentFolder.ToString();
                    Console.WriteLine(myPath);
                    myFiles = FileWorking.LoadFolder(myPath);
                    ArrowMenu myFileMenu = new ArrowMenu(myFiles, 1);
                    int selected = myFileMenu.Select();
                    if (selected == -1)
                    {
                        myPath = oldPath;
                        continue;
                    }
                    string selected_path = myFiles[selected].Path;
                    if (Directory.Exists(selected_path))
                        myPath = selected_path;
                    else
                        try
                        {
                            Process.Start(selected_path);
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("Error while opening file");
                            Thread.Sleep(2000);
                        }
                }
            }
        }
    }
}
