namespace QCP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    public static class Program
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public static void Main(string[] args)
        {
            // Creating a startup object with the basic settings of the program and see if user wants to use defaults.
            Startup startup = JsonSerializer.Deserialize<Startup>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\", "jsons", "startup.json")));
            bool defaults = Startup.UseDefaults(startup);

            List<Correlation> startAndEndPaths = Startup.LoadSettings(defaults, startup);

            List<string> source = Startup.SetDefaultFolder(defaults, startup);

            dict = DictGen.GenerateDictionary(startAndEndPaths, dict);

            // List of the files to move. Made like this to easily implement progress bar
            List<FileMover> fileList = new List<FileMover>();

            try
            {
                PopulateList(fileList, source);
            }
            catch (Exception e)
            {
                Console.WriteLine($"I had a problem, please, send the error to the dev. Here's the error: {e}");
            }

            if (startup.SlowMode)
            {
                foreach (FileMover i in fileList)
                {
                i.TidyUp();
                Thread.Sleep(200);
                }
            }
            else
            {
                Parallel.ForEach(fileList, i => i.TidyUp());
            }

            Startup.RunAtLogin(startup);
            if (!startup.SilentMode)
            {
                Console.WriteLine("All done, see you next time. Press enter to exit.");
                Console.ReadLine();
            }
        }

        private static void PopulateList(List<FileMover> list, List<string> source)
        {
            foreach (string folderWithFiles in source)
            {
                foreach (string fileLocation in Directory.EnumerateFiles(folderWithFiles))
                {
                    string destinationFolder = LocationGenerator(folderWithFiles, fileLocation);
                    string fileName = Path.GetFileNameWithoutExtension(fileLocation);
                    string fileExtension = Path.GetExtension(fileLocation);
                    FileMover fileToMove;
                    if (File.Exists(Path.Combine(destinationFolder, Path.GetFileName(fileLocation))))
                    {
                        int i = 1;
                        while (File.Exists(Path.Combine(destinationFolder, fileName + i + fileExtension)))
                        {
                            i++;
                        }

                        fileToMove = new FileMover(fileLocation, Path.Combine(destinationFolder, fileName + i + fileExtension));
                    }
                    else
                    {
                        fileToMove = new FileMover(fileLocation, Path.Combine(destinationFolder, fileName + fileExtension));
                    }

                    list.Add(fileToMove);
            }
        }

            // Generate destination and create corresponding folder if not present. Returns custom folder if existing.
            static string LocationGenerator(string sourceFolder, string fileName)
            {
                string extension = Path.GetExtension(fileName).Replace(".","");
                var entry = dict.ContainsKey(extension) ? dict[extension] : "Misc";
                string destination;
                if (Directory.Exists(entry))
                {
                    destination = entry;
                }
                else
                {
                    destination = Path.Combine(sourceFolder, entry);
                    Directory.CreateDirectory(destination);
                }

                return destination;
            }
        }
    }
}
