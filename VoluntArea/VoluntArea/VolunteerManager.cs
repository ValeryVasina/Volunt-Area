using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;

namespace VoluntArea
{
    public class VolunteerManager
    {
        IRepository<Event> eventsRepository = Factory.Instance.GetEvent();
        IRepository<User> usersRepository = Factory.Instance.GetUsers();

        //public User AddUser(string userPhoneNumber)
        //{
            
        //}
    }
}
