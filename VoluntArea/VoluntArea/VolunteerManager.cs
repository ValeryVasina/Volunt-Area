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

        public User CheckUser(string login, string password)
        {
            return usersRepository.Items.FirstOrDefault(u => (u.Login == login || u.PhoneNumber == login) && u.Password == u.Password);
        }
        
        public bool CheckUserUnfo(string login, string userName, DateTime birthDt, string password,string email, string phoneNumber)
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
                    UserId = usersRepository.Items.Max(u => u.UserId) + 1
                };
                if(user.IsValid())
                {
                    usersRepository.Add(user);
                    return true;
                }
                else { return false; }
            }
            else
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
        
        // тестовые метод для добавления организаторов мероприятиям 
        public void AddPlannerToExisting()
        {
            eventsRepository.Items.First().Planner = usersRepository.Items.First();
            eventsRepository.Items.First().Type = EventType.Приюты_для_животных;
            eventsRepository.Items.Last().Planner = usersRepository.Items.First();
            eventsRepository.Items.Last().Type = EventType.Форумы_встречи_конференции;
        }
        
        public List<Event> GetEventsForUserAsPlanner(User user)
        {
            List<Event> nullEventList = new List<Event>(); 
            //если мероприятий от конкретного чувака не окажется у нас будет эксепшн, поэтому создаем пустой лист, чтобы его не было
            return eventsRepository.Items.Where(e => e.Planner == user).ToList() ?? nullEventList;
        }
    }
}
