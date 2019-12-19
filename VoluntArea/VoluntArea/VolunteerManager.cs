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
            return usersRepository.Items.FirstOrDefault(u => u.Login == login && u.Password == u.Password);
        }
        
        public bool CheckUserUnfo(string login, string userName, DateTime birthDt, string password,string email, string phoneNumber)
        {
            if (usersRepository.Items.FirstOrDefault(u => u.Login == login || u.PhoneNumber == phoneNumber || u.Email == email) == null)
            {
                User user = new User
                {
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
        public bool CheckPassword(string password1, string password2){return password1 == password2;}
    }
}
