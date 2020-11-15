using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading;

namespace MyCICD
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Clone())
            {
                if (Publish())
                {
                    Run();
                }
            }
        }
        static bool Clone()
        {
            var gitLib = ConfigurationManager.AppSettings["GitLib"];
            var sourcePath = ConfigurationManager.AppSettings["SourcePath"];

            var processStartInfo = new ProcessStartInfo("git", $"clone {gitLib} {sourcePath.TrimEnd('/', '\\')}/{Path.GetFileNameWithoutExtension(gitLib)}") { RedirectStandardOutput = true };

            var process = Process.Start(processStartInfo);
            if (process == null)
            {
                Console.WriteLine("请确认是否安装git");
                return false;
            }
            else
            {
                using (var output = process.StandardOutput)
                {
                    while (!output.EndOfStream)
                    {
                        Console.WriteLine(output.ReadLine());
                    }

                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                }
                Console.WriteLine($"执行时间 :{(process.ExitTime - process.StartTime).TotalMilliseconds} ms");
                return process.ExitCode == 0;

            }
        }

        static bool Run()
        {
            var sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            var publishDir = $"{sourcePath}/publish";
            var result = true;
            foreach (var project in Directory.GetDirectories(publishDir))
            {
                new Thread(RunProject).Start(project);
            }
            return result;
        }
        static void RunProject(object project)
        {
            var processStartInfo = new ProcessStartInfo("dotnet", $" {project}/{Path.GetFileNameWithoutExtension(project.ToString())}.dll") { RedirectStandardOutput = true };

            var process = Process.Start(processStartInfo);
            if (process == null)
            {
                Console.WriteLine("请确认是否安装dotnet sdk");
            }
            else
            {
                using (var output = process.StandardOutput)
                {
                    while (!output.EndOfStream)
                    {
                        Console.WriteLine(output.ReadLine());
                    }

                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                }
                Console.WriteLine($"执行时间 :{(process.ExitTime - process.StartTime).TotalMilliseconds} ms");
                Console.WriteLine($"{project}运行{(process.ExitCode == 0 ? "成功" : "失败")}");
            }
        }

        static bool Publish()
        {
            var sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            var publishProject = ConfigurationManager.AppSettings["PublishProject"];
            var projectPathLists = publishProject.Split(",");
            var projects = GetProjectsPath(sourcePath, projectPathLists);
            var publishDir = $"{sourcePath}/publish";
            var result = true;
            foreach (var project in projects)
            {
                var processStartInfo = new ProcessStartInfo("dotnet", $"publish {project} -o {publishDir}/{Path.GetFileNameWithoutExtension(project)}") { RedirectStandardOutput = true };

                var process = Process.Start(processStartInfo);
                if (process == null)
                {
                    Console.WriteLine("请确认是否安装dotnet sdk");
                    return false;
                }
                else
                {
                    using (var output = process.StandardOutput)
                    {
                        while (!output.EndOfStream)
                        {
                            Console.WriteLine(output.ReadLine());
                        }

                        if (!process.HasExited)
                        {
                            process.Kill();
                        }
                    }
                    Console.WriteLine($"执行时间 :{(process.ExitTime - process.StartTime).TotalMilliseconds} ms");
                    if (process.ExitCode != 0)
                    {
                        Console.WriteLine($"{Path.GetFileNameWithoutExtension(project)}发布失败");
                    }
                    result = result || process.ExitCode == 0;
                }
            }
            return result;
        }
        static string[] GetProjectsPath(string sourcePath, string[] projects)
        {
            var paths = new List<string>();
            foreach (var file in Directory.GetFiles(sourcePath))
            {
                if (projects.Contains(Path.GetFileName(file)))
                {
                    paths.Add(file);
                }

            }
            foreach (var dir in Directory.GetDirectories(sourcePath))
            {
                paths.AddRange(GetProjectsPath(dir, projects));
            }
            return paths.ToArray();
        }
    }
}
