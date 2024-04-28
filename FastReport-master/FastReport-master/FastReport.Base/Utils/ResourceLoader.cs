using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace FastReport.Utils
{
    /// <summary>
    /// Resource loader class.
    /// </summary>
    public static partial class ResourceLoader
    {
        /// <summary>
        /// Gets a stream from specified assembly resource.
        /// </summary>
        /// <param name="assembly">Assembly name.</param>
        /// <param name="resource">Resource name.</param>
        /// <returns>Stream object.</returns>
        public static Stream GetStream(string assembly, string resource)
        {
            string assembly_name = assembly;
            if (assembly == "FastReport")
                assembly_name = typeof(ResourceLoader).Assembly.GetName().Name;

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                AssemblyName name = new AssemblyName(a.FullName);
                if (name.Name == assembly_name)
                {
                    return a.GetManifestResourceStream(assembly + ".Resources." + resource);
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a stream from FastReport assembly resource.
        /// </summary>
        /// <param name="resource">Resource name.</param>
        /// <returns>Stream object.</returns>
        public static Stream GetStream(string resource)
        {
            return GetStream("FastReport", resource);
        }

        /// <summary>
        /// Gets a stream from specified assembly resource and unpacks it.
        /// </summary>
        /// <param name="assembly">Assembly name.</param>
        /// <param name="resource">Resource name.</param>
        /// <returns>Stream object.</returns>
        public static Stream UnpackStream(string assembly, string resource)
        {
            using (Stream packedStream = GetStream(assembly, resource))
            using (Stream gzipStream = new GZipStream(packedStream, CompressionMode.Decompress, true))
            {
                MemoryStream result = new MemoryStream();

                const int BUFFER_SIZE = 4096;
                gzipStream.CopyTo(result, BUFFER_SIZE);

                result.Position = 0;
                return result;
            }
        }

        /// <summary>
        /// Gets a stream from specified FastReport assembly resource and unpacks it.
        /// </summary>
        /// <param name="resource">Resource name.</param>
        /// <returns>Stream object.</returns>
        public static Stream UnpackStream(string resource)
        {
            return UnpackStream("FastReport", resource);
        }
    }
}
