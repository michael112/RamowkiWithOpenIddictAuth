using System;

namespace RamowkiWithOpenIddictAuth.Models
{
    public class Time
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public Time() {}
        public Time(int hours, int minutes) : this()
        {
            this.Hours = hours;
            this.Minutes = minutes;
        }
        public Time(int hours) : this(hours, 0) {}
        public int CompareTo(Time other)
        {
            int hoursCompared = this.Hours.CompareTo(other.Hours);
            if (hoursCompared != 0)
            {
                return hoursCompared;
            }
            else
            {
                return this.Minutes.CompareTo(other.Minutes);
            }
        }
    }
}
