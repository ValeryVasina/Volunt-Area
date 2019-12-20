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

        static FileRepository()
        {
            if (!Directory.Exists(GetPath("Data3")))
                Directory.CreateDirectory(GetPath("Data3"));
            if (!File.Exists(GetPath("Data3/Users.json")))
                using (var sw = new StreamWriter(GetPath("Data3/Users.json")))
                {
                    // ensure file is created
                    sw.Write(@"[{""UserId"":1,""Name"":""Иван Иванов"",""Login"":""Ivan1000"",""BirthDate"":""1995/06/12"",""Email"":""ivanivanov@mail.ru"",""PhoneNumber"":""+79101112233"",""Password"":""1111""},
{""UserId"":2,""Name"":""Мария Сидорова"",""Login"":""Masha23"",""BirthDate"":""1994/07/02"",""Email"":""msidorova@mail.ru"",""PhoneNumber"":""+79112223344"",""Password"":""0000""}]");
                }
            if (!File.Exists(GetPath("Data3/Events.json")))
                using (var sw = new StreamWriter(GetPath("Data3/Events.json")))
                {
                    sw.Write(@"[{""EventId"":1,""EventName"":""Открытие приюта 'Лучший друг'"",""EventDt"":""2020/01/17 20:00:00"",""DurationHours"":3,""Town"":""Москва"",""Address"":""ул.Петрова д.5"",""RequiredPeopleNumber"":7,""Description"":""Ищем добрых людей""},
{""EventId"":2,""EventName"":""Форум для врачей-кардиологов"",""EventDt"":""2020/01/23 17:00:00"",""DurationHours"":4,""Town"":""Москва"",""Address"":""ул.Лизюкова д.31"",""RequiredPeopleNumber"":10,""Description"":""Вкусно покормим""}]");
                }
            
        }

        private static string GetPath(string destination)
            => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), destination);

        public void ReadData(string _fileName)
        {
            using (var sr = new StreamReader(GetPath(_fileName)))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    items = serializer.Deserialize<List<T>>(jsonReader);
                }
            }
        }

        public void SaveData<N>(string _fileName, N data)
        {
            using (var sw = new StreamWriter(GetPath(_fileName)))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, data);
                }
            }
        }
    }
}
