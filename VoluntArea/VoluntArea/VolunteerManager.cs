using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;
using Xamarin.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using VoluntArea.Models;

namespace VoluntArea
{
    public class VolunteerManager
    {
        IRepository<Event> eventsRepository = Factory.Instance.GetEvent();
        IRepository<User> usersRepository = Factory.Instance.GetUsers();

        public List<Event> activeEvents = new List<Event>();
        readonly List<Event> emptyEventList = new List<Event>();

        public VolunteerManager()
        {
            AddPlannerToExistingEvents();
            GetActiveEvents();
            ChangeRatingForUsers();
        }

        public void GetActiveEvents(){activeEvents = eventsRepository.Items.Where(e => e.EventDt > DateTime.Now).ToList() ?? emptyEventList;}

        public User CheckUser(string login, string password)
        {
            return usersRepository.Items.FirstOrDefault(u => (u.Login == login || u.PhoneNumber == login) && u.Password == password);
        }
        
        public bool CheckUserInfoAndAdd(string login, string userName, DateTime birthDt, string password,string email, string phoneNumber)
        {
            if (usersRepository.Items.FirstOrDefault(u => u.Login == login || u.PhoneNumber == phoneNumber || u.Email == email) == null)
            {
                User user = new User
                {
                    Login = login,
                    Name = userName,
                    BirthDate = birthDt,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Password = password,
                    Rating = new Rating()
                };
                if(user.IsValid())
                {
                    user.UserId = usersRepository.Items.Max(u => u.UserId) + 1;
                    usersRepository.Add(user);
                    return true;
                }
                else { return false; }
            }
            return false;
        }

        public DateTime? CheckDates(string date)
        {
            try
            {
                DateTime correctDate = Convert.ToDateTime(date);
                return correctDate;
            }
            catch(FormatException){return null;}
        }
        public bool CheckPhoneNumberFormat(string phoneNumber)
        {
            string phoneFormat = @"\+7\d{10}";
            try{return Regex.IsMatch(phoneNumber, phoneFormat);}
            catch (ArgumentNullException) { return false; }
        }
        public bool CheckPassword(string password1, string password2){return password1!=null ? password1 == password2: false;}

        // тестовый метод для добавления организаторов мероприятиям 
        public void AddPlannerToExistingEvents()
        {
            eventsRepository.Items.First().Planner = usersRepository.Items.First();
            eventsRepository.Items.First().Type = EventType.Приюты_для_животных;
            eventsRepository.Items.Last().Planner = usersRepository.Items.First();
            eventsRepository.Items.Last().Type = EventType.Форумы_встречи_конференции;

            foreach (var ev in eventsRepository.Items)
            { ev.Rating = new Rating(); }
            eventsRepository.Items.First().Rating.Value = 17;
            eventsRepository.Items.Last().Rating.Value = 17;

            foreach (var user in usersRepository.Items)
            { user.Rating = new Rating(); }
            usersRepository.Items.First().Rating.Value = 12;
            usersRepository.Items.Last().Rating.Value = 3;
        }

        public List<Event> GetActiveEventsForUserAsPlanner(User user)
        {
            //если мероприятия от конкретного чувака не окажется у нас будет эксепшн, поэтому создаем пустой лист, чтобы его не было
            return eventsRepository.Items.Where(e => e.Planner == user && e.EventDt > DateTime.Now).ToList() ?? emptyEventList;
        }
        public List<Event> GetPastEventsForUserAsPlanner(User user)
        {
            return eventsRepository.Items.Where(e => e.Planner == user && e.EventDt < DateTime.Now).ToList() ?? emptyEventList;
        }

        // устанавливаем границы для длительности мероприятия
        public bool CheckDurationForEvent(string hours)
        {
            int duration = 0;
            int maxHours = 10;
            int minHours = 1;
            Int32.TryParse(hours, out duration);
            return (minHours <= duration)&&(maxHours >= duration);
        }
        //проверяем правильность данных для необходимого количества волонтеров на мероприятие
        public bool CheckPeopleNumber(string people)
        {
            int requiredVolunteers = 0;
            Int32.TryParse(people, out requiredVolunteers);
            return requiredVolunteers > 0;
        }
        public bool CheckEventInfoAndAdd(User planner, string name, string town, string address, DateTime eventDt,
            int duration, int peopleNumber, string description)
        {
            Event newEvent = new Event
            {
                Planner = planner,
                EventName = name,
                Town = town,
                Address = address,
                EventDt = eventDt,
                Description = description,
                DurationHours = duration,
                RequiredPeopleNumber = peopleNumber,
                Rating = new Rating(),
                Volunteers = new List<User>()
            };
            if(newEvent.IsValid())
            {
                newEvent.EventId = eventsRepository.Items.Max(e => e.EventId) + 1;
                eventsRepository.Add(newEvent);
                GetActiveEvents();
                return true;
            }
            return false;
        }

        //считаем рейтинг при входе в приложение или обновлении страницы
        public void ChangeRatingForUsers()
        {
            List<Event> newPastEvents = eventsRepository.Items.Except(activeEvents).ToList() ?? emptyEventList;

            newPastEvents.ForEach(e => e.Volunteers.ForEach(v => v.Rating.Value += e.DurationHours));
        }

        public bool AddUserToEvent(User user, Event activeEvent)
        {
            if (activeEvent.Volunteers.Count < activeEvent.RequiredPeopleNumber && !activeEvent.Volunteers.Contains(user))
            {
                activeEvent.Volunteers.Add(user);
                return true;
            }
            return false;
        }

        //вытаскиваем текущие ивенты для юзера, которые он собирается посетить
        public List<Event> GetActiveEventsForUserToAttend(User user)
        {
            return eventsRepository.Items.Where(e => e.Volunteers.Contains(user) && e.EventDt > DateTime.Now).ToList() ?? emptyEventList;
        }
        //вытаскиваем прошедшие ивенты для юзера
        public List<Event> GetPastEventsForUser(User user)
        {
            return eventsRepository.Items.Where(e => e.Volunteers.Contains(user) && e.EventDt < DateTime.Now).ToList() ?? emptyEventList;
        }
        public void RemoveUserFromActiveEvent(User user, Event activeEvent)
        {
            activeEvent.Volunteers.Remove(user);
        }
        // делаем выборку по городу
        public List<Event> GetEventsForTown(string town)
        {
            return eventsRepository.Items.Where(e => e.Town.ToLower() == town.ToLower()).ToList() ?? emptyEventList;
        }
        
    }
}
