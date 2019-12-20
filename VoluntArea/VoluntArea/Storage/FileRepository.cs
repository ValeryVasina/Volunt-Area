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
            SaveData<List<T>>(fileName, items);
        }

        static FileRepository()
        {
            if (!Directory.Exists(GetPath("AppData")))
                Directory.CreateDirectory(GetPath("AppData"));
            if (!File.Exists(GetPath("AppData/Users.json")))
                using (var sw = new StreamWriter(GetPath("AppData/Users.json")))
                {
                    sw.Write(@"[{""UserId"":1,""Name"":""Иван Иванов"",""Login"":""Ivan1000"",""BirthDate"":""1995/06/12"",""Email"":""ivanivanov@mail.ru"",""PhoneNumber"":""+79101112233"",""Password"":""1111""},
{""UserId"":2,""Name"":""Мария Сидорова"",""Login"":""Masha23"",""BirthDate"":""1994/07/02"",""Email"":""msidorova@mail.ru"",""PhoneNumber"":""+79112223344"",""Password"":""0000""},
{""UserId"":3,""Name"":""Сергей Лихачев"",""Login"":""Sergey12"",""BirthDate"":""1996/09/02"",""Email"":""slikchachev@mail.ru"",""PhoneNumber"":""+79112223355"",""Password"":""1234""}]");
                }
            if (!File.Exists(GetPath("AppData/Events.json")))
                using (var sw = new StreamWriter(GetPath("AppData/Events.json")))
                {
                    sw.Write(@"[{""EventId"":1,""EventName"":""Открытие приюта 'Лучший друг'"",""EventDt"":""2020/01/17 20:00:00"",""DurationHours"":3,""Town"":""Москва"",""Address"":""ул.Петрова д.5"",""RequiredPeopleNumber"":7,""Description"":""Ищем добрых людей""},
{""EventId"":2,""EventName"":""Форум для врачей-кардиологов"",""EventDt"":""2020/01/23 17:00:00"",""DurationHours"":4,""Town"":""Москва"",""Address"":""ул.Лизюкова д.31"",""RequiredPeopleNumber"":10,""Description"":""Вкусно покормим""},
{""EventId"":3,""EventName"":""Организация дня города"",""EventDt"":""2019/11/11 21:00:00"",""DurationHours"":8,""Town"":""Рязань"",""Address"":""ул.Лихачева д.9"",""RequiredPeopleNumber"":15,""Description"":""Будет весело,зови друзей!""},
{""EventId"":4,""EventName"":""Субботник в Парке 'Орленок'"",""EventDt"":""2019/04/23 12:00:00"",""DurationHours"":4,""Town"":""Воронеж"",""Address"":""ул.Лизюкова д.17"",""RequiredPeopleNumber"":20,""Description"":""Подготовь наш город к лету с нами!""}]");
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
