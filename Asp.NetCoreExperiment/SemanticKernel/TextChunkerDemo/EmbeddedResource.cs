using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextChunkerDemo
{
    internal static class EmbeddedResource
    {
        private static readonly string? s_namespace = typeof(EmbeddedResource).Namespace;

        internal static string Read(string fileName)
        {
            // Get the current assembly. Note: this class is in the same assembly where the embedded resources are stored.
            Assembly? assembly = typeof(EmbeddedResource).GetTypeInfo().Assembly;
            if (assembly == null) { throw new Exception($"[{s_namespace}] {fileName} assembly not found"); }

            // Resources are mapped like types, using the namespace and appending "." (dot) and the file name
            var resourceName = $"{s_namespace}." + fileName;
            using Stream? resource = assembly.GetManifestResourceStream(resourceName);
            if (resource == null) { throw new Exception($"{resourceName} resource not found"); }

            // Return the resource content, in text format.
            using var reader = new StreamReader(resource);
            return reader.ReadToEnd();
        }

        internal static Stream? ReadStream(string fileName)
        {
            // Get the current assembly. Note: this class is in the same assembly where the embedded resources are stored.
            Assembly? assembly = typeof(EmbeddedResource).GetTypeInfo().Assembly;
            if (assembly == null) { throw new Exception($"[{s_namespace}] {fileName} assembly not found"); }

            // Resources are mapped like types, using the namespace and appending "." (dot) and the file name
            var resourceName = $"{s_namespace}." + fileName;
            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
