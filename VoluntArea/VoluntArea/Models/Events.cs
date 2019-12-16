using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;

namespace VoluntArea
{
    class Events : IEntity
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDt { get; set; }
        public User Planner { get; set; }
        public string Location { get; set; }
        public TimeSpan Duration { get; set; }
        public int RequiredPeopleNumber { get; set; }
        public string Description { get; set; }

        //проверяем на соответствие "валидности" - непустое имя, положительное кол-во человек, дата позже сейчас
        public bool IsValid()
        {
            DateTime nowDt = DateTime.Now;
            return string.IsNullOrWhiteSpace(EventName)&&(RequiredPeopleNumber > 0)&&(EventDt.CompareTo(nowDt) > 0);
        }
    }
}
