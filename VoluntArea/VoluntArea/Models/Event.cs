using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;
using VoluntArea.Models;

namespace VoluntArea
{
    public enum EventType
    {
        Субботник,
        Праздник,
        Помощь_престарелым,
        Детские_дома,
        Приюты_для_животных,
        Форумы_встречи_конференции
    }
    public class Event : IEntity
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDt { get; set; }
        public User Planner { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public EventType? Type { get; set; }
        public int DurationHours { get; set; }
        public int RequiredPeopleNumber { get; set; }
        public string Description { get; set; }
        public List<User> Volunteers { get; set; }
        public int PeopleSignedUp { get; set; }
        public Rating Rating { get; set; }
        //проверяем на соответствие "валидности" - непустое имя, положительное кол-во человек, дата позже сейчас
        public bool IsValid()
        {
            DateTime nowDt = DateTime.Now;
            return !string.IsNullOrWhiteSpace(EventName)||(RequiredPeopleNumber <= 0)||(EventDt.CompareTo(nowDt) < 0)
                ||string.IsNullOrWhiteSpace(Town)||string.IsNullOrWhiteSpace(Address)||string.IsNullOrWhiteSpace(Description);
        }
    }
}
