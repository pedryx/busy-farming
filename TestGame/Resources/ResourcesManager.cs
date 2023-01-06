using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TestGame.Resources
{
    internal abstract class ResourcesManager<T>
    {
        private Dictionary<string, T> resources = new();

        public string FileExtension { get; private set; }

        public string ResourceDirectory { get; private set; }

        public ResourcesManager(string fileExtension, string directory)
        {
            FileExtension = fileExtension;
            ResourceDirectory = directory;
        }

        public abstract T Load(string path);

        public void LoadAll()
        {
            var files = Directory.GetFiles(
                ResourceDirectory,
                $"*.{FileExtension}",
                SearchOption.AllDirectories
            );

            foreach (var file in files)
            {
                string name = file.Split('/', '\\').Last().Split('.').First();
                T item = Load(file);

                resources.Add(name, item);
            }
        }

        public T this[string name]
        {
            get
            {
                if (!resources.ContainsKey(name))
                    throw new Exception($"Resource with name {name} not found!");

                return resources[name];
            }
        }
    }
}
