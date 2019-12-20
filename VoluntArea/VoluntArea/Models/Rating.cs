using System;
using System.Collections.Generic;
using System.Text;

namespace VoluntArea.Models
{
    public class Rating
    {
        public int Value { get; set; }

        const int thresholdToPlanEvent = 10;

        public bool CheckUserRatingToPlan()
        {
            return Value >= thresholdToPlanEvent;
        }

    }
}
