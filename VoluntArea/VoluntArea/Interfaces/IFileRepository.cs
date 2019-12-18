using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace VoluntArea.Interfaces
{
    public interface IFileRepository<T>: IRepository<T>
    {
        void SaveData(string _fileName);
        void ReadData(string _fileName);
    }
}
