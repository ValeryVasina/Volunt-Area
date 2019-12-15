using System;
using System.Collections.Generic;
using System.Text;

namespace VoluntArea
{
    class Events
    {
        public string EventName { get; set; }
        public DateTime EventDt { get; set; }
        public User Planner { get; set; }
        public string Location { get; set; }
        public TimeSpan Duration { get; set; }
        public int RequiredPeopleNumber { get; set; }
        public string Description { get; set; }

    }
}
