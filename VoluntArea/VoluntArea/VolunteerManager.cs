using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;
using Xamarin.Forms;
using System.Linq;

namespace VoluntArea
{
    public class VolunteerManager
    {
        IRepository<Event> eventsRepository = Factory.Instance.GetEvent();
        IRepository<User> usersRepository = Factory.Instance.GetUsers();

        public User CheckUser(string login, string password)
        {
            return usersRepository.Items.FirstOrDefault(u => u.PhoneNumber == login && u.Password == u.Password);
        }
        
    }
}
