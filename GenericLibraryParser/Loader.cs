namespace GenericLibraryParser
{
    using System;
    using System.Runtime.Loader;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    public class Loader
    {
        private readonly string _libsFolder;
        public Loader(string librariesFolder) {
            if (Directory.Exists(librariesFolder))
            {
                _libsFolder = librariesFolder;
            }
            else
            {
                Console.WriteLine($"Folder:{librariesFolder} not found, creating folder");
                Directory.CreateDirectory(librariesFolder);
            }
            


        }

        public IEnumerable<Type> LoadTypesDerivedFrom(Type type) {
            DirectoryInfo d = new DirectoryInfo(_libsFolder);
            var files = d.GetFiles("*.dll");
            var typesResult = new List<Type>();
            foreach(var f in files)
            {
                var myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(f.FullName);
                typesResult.AddRange(myAssembly.GetTypes().Where(z => z.GetInterfaces().Contains(type)));

            }
            return typesResult;
        }
        
    }
}
