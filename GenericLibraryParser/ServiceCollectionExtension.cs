

namespace GenericLibraryParser
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTransientFromLibrary<T>(this IServiceCollection sc, string libraryPath)
        {
            var loader = new Loader(libraryPath);
            var type = typeof(T);
            var types = loader.LoadTypesDerivedFrom(type);
            foreach (var t in types)
            {
                sc.AddTransient(type, t);
            }

            return sc;
        }
    }
}
