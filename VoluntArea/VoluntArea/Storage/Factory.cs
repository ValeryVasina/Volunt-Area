using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;
using VoluntArea.Storage;

namespace VoluntArea
{
    // один источник
    public class Factory
    {
        private static Factory instance;
        private Factory() { }
        public static Factory Instance => instance ?? (instance = new Factory());

        // считываем данные с json файлов в списки
        private IRepository<Event> eventsRepository = new FileRepository<Event>("data/events.json");
        private IRepository<User> usersRepository = new FileRepository<User>("data/users.json");
        
        //достаем списки
        public IRepository<Event> GetEvent() => eventsRepository;
        public IRepository<User> GetUsers() => usersRepository;
    }
}
