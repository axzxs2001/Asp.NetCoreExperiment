using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace DynamicLoadDll
{
    /// <summary>
    /// Assembly加载器
    /// </summary>
    public static class AssemblyLoader
    {
        public static Assembly LoadFromAssemblyPath(string assemblyFullPath)
        {
            var fileNameWithOutExtension = Path.GetFileNameWithoutExtension(assemblyFullPath);
            var fileName = Path.GetFileName(assemblyFullPath);
            var directory = Path.GetDirectoryName(assemblyFullPath);

            var inCompileLibraries = DependencyContext.Default.CompileLibraries.Any(l => l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));
            var inRuntimeLibraries = DependencyContext.Default.RuntimeLibraries.Any(l => l.Name.Equals(fileNameWithOutExtension, StringComparison.OrdinalIgnoreCase));

            var fileStream = new FileStream(assemblyFullPath, FileMode.Open, FileAccess.Read);
            var assembly = (inCompileLibraries || inRuntimeLibraries)
                ? Assembly.Load(File.ReadAllBytes(assemblyFullPath))
                : AssemblyLoadContext.Default.LoadFromStream(fileStream);
            fileStream.Close();

            if (assembly != null)
            {
                LoadReferencedAssemblies(assembly, fileName, directory);
            }
            return assembly;
        }

        private static void LoadReferencedAssemblies(Assembly assembly, string fileName, string directory)
        {
            var filesInDirectory = Directory.GetFiles(directory).Where(x => x != fileName).Select(x => Path.GetFileNameWithoutExtension(x)).ToList();
            var references = assembly.GetReferencedAssemblies();
            foreach (var reference in references)
            {
                if (filesInDirectory.Contains(reference.Name))
                {
                    var loadFileName = reference.Name + ".dll";
                    var path = Path.Combine(directory, loadFileName);
                    var loadedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                    if (loadedAssembly != null)
                    {
                        LoadReferencedAssemblies(loadedAssembly, loadFileName, directory);
                    }
                }
            }

        }

    }
}
