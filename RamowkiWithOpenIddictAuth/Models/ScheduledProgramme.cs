using System;

namespace RamowkiWithOpenIddictAuth.Models
{
    public class ScheduledProgramme
    {
        public Guid Id { get; set; }
        public Programme Programme { get; set; } 
        public Day Day { get; set; }
        public Time BeginTime { get; set; }

        public ScheduledProgramme() {}
        public ScheduledProgramme(Programme programme, Day day, Time beginTime) : this()
        {
            this.Programme = programme;
            this.Day = day;
            this.BeginTime = beginTime;
        }
        public ScheduledProgramme(Guid id, Programme programme, Day day, Time beginTime) : this(programme, day, beginTime)
        {
            this.Id = id;
        }
    }
}
