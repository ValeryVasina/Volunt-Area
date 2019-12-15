using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;

namespace VoluntArea
{
    class User : IEntity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        //проверка на наличие имени
        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(Name);
        }
    }
}
