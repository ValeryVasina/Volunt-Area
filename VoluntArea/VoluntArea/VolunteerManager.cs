using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;
using Xamarin.Forms;
using System.Linq;
using System.Text.RegularExpressions;

namespace VoluntArea
{
    public class VolunteerManager
    {
        IRepository<Event> eventsRepository = Factory.Instance.GetEvent();
        IRepository<User> usersRepository = Factory.Instance.GetUsers();

        public List<Event> activeEvents = new List<Event>();
        DateTime now = DateTime.Now;
        List<Event> nullEventList = new List<Event>();

        public VolunteerManager()
        {
            GetActiveEvents();
            AddPlannerToExistingEvents();
            ChangeRatingForUsers();
        }

        public void GetActiveEvents(){activeEvents = eventsRepository.Items.Where(e => e.EventDt > now).ToList();}

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
                    Password = password
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
        }
        
        public List<Event> GetEventsForUserAsPlanner(User user)
        {
            //если мероприятия от конкретного чувака не окажется у нас будет эксепшн, поэтому создаем пустой лист, чтобы его не было
            return eventsRepository.Items.Where(e => e.Planner == user).ToList() ?? nullEventList;
        }
        public void ChangeRatingForUsers()
        {
            List<Event> newPastEvents = eventsRepository.Items.Except(activeEvents).ToList()?? nullEventList;

            foreach(var pastEvent in newPastEvents.Where(e => e.Volunteers != null))
            {
                pastEvent.Volunteers.ForEach(v => v.Rating += pastEvent.DurationHours);
            }
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
                RequiredPeopleNumber = peopleNumber
            };
            if(newEvent.IsValid())
            {
                newEvent.EventId = eventsRepository.Items.Max(e => e.EventId) + 1;
                eventsRepository.Add(newEvent);
                activeEvents.Add(newEvent);
                return true;
            }
            return false;
        }
    }
}
