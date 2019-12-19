using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;

namespace VoluntArea
{
    public class User : IEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        //проверка на наличие имени и возраста >= 16
        public bool IsValid()
        {
            DateTime nowDt = DateTime.Now;
            TimeSpan userAge = nowDt - BirthDate;
            double threshold = 16.0;
            return string.IsNullOrEmpty(Login)&&string.IsNullOrWhiteSpace(Name)&&((userAge.TotalDays/365) >= threshold);
        }
    }
}
