using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;

namespace VoluntArea
{
    class Events : IEntity
    {
        public string EventName { get; set; }
        public DateTime EventDt { get; set; }
        public User Planner { get; set; }
        public string Location { get; set; }
        public TimeSpan Duration { get; set; }
        public int RequiredPeopleNumber { get; set; }
        public string Description { get; set; }

        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(EventName)&&(RequiredPeopleNumber > 0);
        }
    }
}
