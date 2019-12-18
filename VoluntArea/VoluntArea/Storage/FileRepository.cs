using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace VoluntArea.Storage
{
    class FileRepository<T> : Repository<T>, IFileRepository<T>
    {
        public FileRepository(string fileName)
        {
            ReadData(fileName);
        }
        public void ReadData(string _fileName)
        {
            using (var sr = new StreamReader(_fileName))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    items = serializer.Deserialize<List<T>>(jsonReader);
                }
            }
        }

        public void SaveData(string _fileName)
        {
            throw new NotImplementedException();
        }
    }
}
