using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace MyCICD
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                if (Clone())
                {
                    if (Publish())
                    {
                        Run();
                    }
                }
                                
                Thread.Sleep(30000);
            }
        }
        static bool Clone()
        {
           
            var gitLib = ConfigurationManager.AppSettings["GitLib"];
            var sourcePath = ConfigurationManager.AppSettings["SourcePath"];

            var SourceDir = $"{sourcePath.TrimEnd('/', '\\') }/{ Path.GetFileNameWithoutExtension(gitLib)} ";
            if (Directory.Exists(SourceDir))
            {
                var processStartInfo = new ProcessStartInfo("git", $"fetch origin") { RedirectStandardOutput = true };
                var process = Process.Start(processStartInfo);
                if (process == null)
                {
                    Console.WriteLine("请确认是否安装git");
                    return false;
                }
                else
                {
                    Console.WriteLine("开始fetch");
                    using (var output = process.StandardOutput)
                    {
                        var resultBuilder = new StringBuilder();
                        while (!output.EndOfStream)
                        {
                            resultBuilder.AppendLine(output.ReadLine());
                        }
                        Console.WriteLine(resultBuilder);
                        if (resultBuilder.Length == 0)
                        {
                            Console.WriteLine("没有要同步的代码，结束fetch");
                            return false;
                        }
                        if (!process.HasExited)
                        {
                            process.Kill();
                        }
                    }
                    Console.WriteLine($"执行时间 :{(process.ExitTime - process.StartTime).TotalMilliseconds} ms");
                    Console.WriteLine("结束fetch");
                    return process.ExitCode == 0;

                }

            }
            else
            {
                Console.WriteLine("开始Clone");
                var processStartInfo = new ProcessStartInfo("git", $"clone {gitLib} {SourceDir}") { RedirectStandardOutput = true };

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
                    Console.WriteLine("结束Clone");
                    return process.ExitCode == 0;

                }
            }
        }

        static bool Run()
        {
            Console.WriteLine("开始运行");
            var sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            var publishDir = $"{sourcePath}/publish";
            var result = true;
            foreach (var project in Directory.GetDirectories(publishDir))
            {
                new Thread(RunProject).Start(project);
            }
            Console.WriteLine("结束运行");
            return result;
        }
        static void RunProject(object project)
        {
            var projectPath = project.ToString();
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe ";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.Arguments = $"/k cd /d {projectPath}";
            process.Start();
            process.StandardInput.WriteLine($"dotnet {Path.GetFileNameWithoutExtension(projectPath)}.dll");
            Console.WriteLine($"{Path.GetFileNameWithoutExtension(projectPath)}运行成功");
        }

        static bool Publish()
        {
            Console.WriteLine("开始Publish");
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
            Console.WriteLine("结束Publish");
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
