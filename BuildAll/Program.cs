using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BuildAll
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var projectToBuild in GetAppsToBuild())
            {
                BuildProject(projectToBuild);
            }

            Console.ReadKey(true);
        }

        static IEnumerable<FileInfo> GetAppsToBuild()
        {
            var currentFolder = Directory.GetCurrentDirectory();
            string appsFileLocation = Path.Combine(currentFolder, "apps.json");

            string appsFileContents = File.ReadAllText(appsFileLocation);

            var relativePaths = JsonConvert.DeserializeObject<string[]>(appsFileContents);

            return relativePaths.Select(location =>
                new FileInfo(Path.Combine(currentFolder,
                    location.Replace('/', Path.DirectorySeparatorChar)
                ))
            );
        }

        static void BuildProject(FileInfo projectLocation)
        {
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = projectLocation.DirectoryName,
                UseShellExecute = false,
                FileName = "dotnet",
                Arguments = projectLocation.Name,
                RedirectStandardOutput = true
            };

            var proc = Process.Start(startInfo);

            proc.WaitForExit();
        }
    }
}
