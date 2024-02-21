using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NoteApp
{
    class Program
    {
        private static string NoteDirectory =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Notes");


        private static void ReadCommand()
        {
            Console.Write(Directory.GetDirectoryRoot(NoteDirectory));
            string cmd = Console.ReadLine();

            switch(cmd.ToLower())
            {
                case "new":
                    NewNote();
                    Main(null);
                    break;
                case "edit":
                    EditNote();
                    Main(null);
                    break;
                case "read":
                    ReadNote();
                    Main(null);
                    break;
                case "delete":
                    DeleteNote();
                    Main(null);
                    break;
                case "show":
                    ShowNotes();
                    Main(null);
                    break;
                case "dir":
                    NotesDirectory();
                    Main(null);
                    break;
                case "cls":
                    Console.Clear();
                    Main(null);
                    break;
                case "exit":
                    Exit();
                    break;
                default:
                    CommandAvailable();
                    Main(null);
                    break;
            }
        }

        private static void DeleteNote()
        {
            Console.Write("Please enter file name: ");
            string fileName = Console.ReadLine();
            string fullPath = Path.Combine(NoteDirectory, fileName);

            if (File.Exists(fullPath))
            {
                Console.WriteLine(Environment.NewLine + "Are you sure you wish to delete this file? Y/N\n");
                string confirm = Console.ReadLine().ToLower();

                if (confirm == "y")
                {
                    try
                    {
                        File.Delete(fullPath);
                        Console.WriteLine("File has been deleted\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("File not deleted following error occured: " + ex.Message);
                    }
                }
                else if (confirm == "n")
                {
                    Main(null);
                }
                else
                {
                    Console.WriteLine("Invalid command\n");
                    DeleteNote();
                }
            }
            else
            {
                Console.WriteLine("File does not exist\n");
                DeleteNote();
            }
        }

        private static void CommandAvailable()
        {
            Console.WriteLine(" New - Create a new note\n Edit - Edit a note\n Read -  Read a note\n ShowNotes - List all notes\n Exit - Exit the application\n Dir - Opens note directory\n Help - Shows this help message\n");
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }

        private static void NotesDirectory()
        {
            Process.Start("explorer.exe", NoteDirectory);
        }

        private static void ShowNotes()
        {
            string NoteLocation = NoteDirectory;
            DirectoryInfo Dir = new DirectoryInfo(NoteLocation);

            if (Directory.Exists(NoteLocation))
            {
                FileInfo[] NoteFiles = Dir.GetFiles("*.xml");

                if (NoteFiles.Count() != 0)
                {
                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop + 2);
                    Console.WriteLine("+------------+");
                    foreach (var item in NoteFiles)
                    {
                        Console.WriteLine("  " + item.Name);
                    }
                    Console.WriteLine(Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("No notes found.\n");
                }
            }
            else
            {
                Console.WriteLine(" Directory does not exist.....creating directory\n");
                Directory.CreateDirectory(NoteLocation);
                Console.WriteLine(" Directory: " + NoteLocation + " created successfully.\n");
            }
        }

        private static void ReadNote()
        {
            Console.Write("Please enter file name: ");
            string fileName = Console.ReadLine().ToLower();
            string fullPath = Path.Combine(NoteDirectory, fileName);


            if (File.Exists(fullPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fullPath);
                Console.WriteLine(doc.SelectSingleNode("//Header").InnerText);
            }
            else
            {
                Console.WriteLine("File not found\n");
            }
        }

        private static void EditNote()
        {
            Console.Write("Please enter file name: ");
            string fileName = Console.ReadLine().ToLower();

            string fullPath = Path.Combine(NoteDirectory, fileName);

            if (File.Exists(fullPath))
            {
                XmlDocument doc = new XmlDocument();    

                try
                {
                    doc.Load(fullPath);
                    Console.WriteLine(doc.SelectSingleNode("//Header").InnerText);
                    string ReadInput = Console.ReadLine();

                    if (ReadInput.ToLower() == "cancel")
                    {
                        Main(null);
                    }
                    else
                    {
                        string newText = doc.SelectSingleNode("//Header").InnerText = ReadInput;

                        doc.Save(fullPath);
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("Could not edit note following error occurred: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File not found\n");
            }
        }

        private static void NewNote()
        {
            Console.Write("Please enter Note: ");
            string input = Console.ReadLine();
            Console.Write("Please enter Title: ");
            string title = Console.ReadLine();
            Console.Write("Please enter Description: ");
            string description = Console.ReadLine();

            XmlWriterSettings NoteSettings = new XmlWriterSettings();

            NoteSettings.CheckCharacters = false;
            NoteSettings.ConformanceLevel = ConformanceLevel.Auto;
            NoteSettings.Indent = true;

            string fileName = DateTime.Now.ToString("yy-MM-dd") + ".html";

            string fullPath = Path.Combine(NoteDirectory, fileName);

            // Check if the directory exists, and create it if it doesn't
            if (!Directory.Exists(NoteDirectory))
            {
                Directory.CreateDirectory(NoteDirectory);
            }


            using (XmlWriter NewNote = XmlWriter.Create(fullPath, NoteSettings))
            {
                NewNote.WriteStartDocument();
                NewNote.WriteStartElement("Header");
                NewNote.WriteElementString("h1", input);
                NewNote.WriteElementString("h2", title);
                NewNote.WriteElementString("h3", description);

                NewNote.WriteEndElement();

                NewNote.Flush();
                NewNote.Close();
            }


            // Create the XML content with custom styling attributes
            //string xmlContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            //<Note>
            //    <Header>
            //        <h1 style=""color: red;"" >{input}</h1>
            //        <h2 style=""color: blue;"" >{title}</h2>
            //        <h3 style=""color: yellow;"" >{description}</h3>
            //    </Header>
            //</Note>";

            // Write the XML content to the file
            //File.WriteAllText(fullPath, xmlContent);

            Console.WriteLine($"Note saved to: {fullPath}");
        }

        static void Main(string[] args)
        {
            ReadCommand();
            Console.ReadLine();
        }
    }
}
