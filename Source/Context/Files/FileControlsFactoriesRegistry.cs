using JsonFlier.Context.Files.ConcreteFactories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonFlier.Context.Files
{
    public static class FileControlsFactoriesRegistry
    {
        public static List<IFileControlsFactory> RegisteredFactories { get; } = new List<IFileControlsFactory>();

        static FileControlsFactoriesRegistry()
        {
            RegisterFactory(new JsonLoggerFileContextFactory());
            RegisterFactory(new PlainTextFactory());
        }

        public static IFileControlsFactory DetectDefaultFactory(string filePath)
        {
            var factory = RegisteredFactories.FirstOrDefault(factory => factory.CanOpenFile(filePath));

            if (factory == null)
                throw new Exception("Cannot find suitable factory. All factories returned false for CanOpenFile(string)");

            return factory;
        }

        private static void RegisterFactory(IFileControlsFactory tabControlFactory)
        {
            RegisteredFactories.Add(tabControlFactory);
        }
    }
}
