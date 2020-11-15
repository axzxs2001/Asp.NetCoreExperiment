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
            var processIDs = new int[0];
            while (true)
            {
                if (Clone())
                {
                    if (Publish(processIDs))
                    {
                        processIDs = Run();
                    }
                }
                Thread.Sleep(30000);
            }
        }

        /// <summary>
        /// git 克隆
        /// </summary>
        /// <returns></returns>
        static bool Clone()
        {
            var gitLib = ConfigurationManager.AppSettings["GitLib"];
            var sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            var sourceDir = $"{sourcePath.TrimEnd('/', '\\') }/{ Path.GetFileNameWithoutExtension(gitLib)} ";
            //存在就拉取代码，不存在就克隆
            if (Directory.Exists(sourceDir))
            {
                return Pull(sourceDir);
            }
            else
            {
                return Clone(gitLib, sourceDir);
            }
        }
        /// <summary>
        /// 克隆代码
        /// </summary>
        /// <param name="gitLib">git库</param>
        /// <param name="sourceDir">本地保存路径</param>
        /// <returns></returns>
        static bool Clone(string gitLib, string sourceDir)
        {
            Console.WriteLine("开始Clone");
            var processStartInfo = new ProcessStartInfo("git", $"clone {gitLib} {sourceDir}") { RedirectStandardOutput = true };

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
        /// <summary>
        /// 拉取项目
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <returns></returns>
        static bool Pull(string sourceDir)
        {
            Console.WriteLine("开始Fetch");
            var processStartInfo = new ProcessStartInfo("git", $"pull origin")
            {
                RedirectStandardOutput = true,
                WorkingDirectory = sourceDir,
            };

            var process = Process.Start(processStartInfo);
            using (var output = process.StandardOutput)
            {
                var resultBuilder = new StringBuilder();
                while (!output.EndOfStream)
                {
                    resultBuilder.AppendLine(output.ReadLine());
                }
                Console.WriteLine(resultBuilder);
                if (!process.HasExited)
                {
                    process.Kill();
                }
                if (resultBuilder.ToString() != "Already up to date.\r\n")
                {
                    Console.WriteLine("结束Fetch");
                    return true;
                }
                else
                {
                    Console.WriteLine("结束Fetch，远程仓库没有更新");
                    return false;
                }
            }
        }



        #region 发布项目
        /// <summary>
        /// 发布项目
        /// </summary>
        /// <returns></returns>
        static bool Publish(int[] processIDs)
        {
            Console.WriteLine("开始Publish");
            var sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            var publishProject = ConfigurationManager.AppSettings["PublishProject"];
            var projectPathLists = publishProject.Split(",");
            var projects = GetProjectsPath(sourcePath, projectPathLists);
            var publishDir = $"{sourcePath}/publish";
            var result = true;
            //关闭之前进程
            foreach (var processid in processIDs)
            {
                Process.GetProcessById(processid).Kill();
            }
            //发布项目
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
        /// <summary>
        /// 查找项目
        /// </summary>
        /// <param name="sourcePath">源码路径</param>
        /// <param name="projects">项目集</param>
        /// <returns></returns>
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
        #endregion

        #region 运行项目
        /// <summary>
        /// 运行项目
        /// </summary>
        /// <returns></returns>
        static int[] Run()
        {
            Console.WriteLine("开始运行");
            var sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            var publishDir = $"{sourcePath}/publish";
            var proceddIDs = new List<int>();
            foreach (var projectPath in Directory.GetDirectories(publishDir))
            {
                var processStartInfo = new ProcessStartInfo("dotnet", $"{Path.GetFileNameWithoutExtension(projectPath)}.dll")
                {
                    RedirectStandardOutput = true,
                    WorkingDirectory = projectPath,
                };
                var process = Process.Start(processStartInfo);
                proceddIDs.Add(process.Id);
            }
            Console.WriteLine("结束运行");
            return proceddIDs.ToArray();
        }

        #endregion
    }
}
