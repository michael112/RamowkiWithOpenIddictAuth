using System;
using System.Collections.Generic;

using RamowkiWithOpenIddictAuth.DTO;
using RamowkiWithOpenIddictAuth.Models;

namespace RamowkiWithOpenIddictAuth.Services.ScheduleService
{
    public interface IScheduleService
    {
        void CreateSchedule(ScheduledProgrammeJson schedule, String programme);
        void CreateScheduleAndProgramme(ScheduledProgrammeJson schedule, ProgrammeJson programme);
        void UpdateSchedule(string scheduleID, ScheduledProgrammeJson programme);
        void SwitchProgramme(string scheduleID, string programmeID);
        void DeleteSchedule(string scheduleID);
        ScheduledProgramme GetScheduledProgramme(string scheduleID);
        IEnumerable<ScheduledProgramme> GetScheduledProgrammeListByWeekDay(int dayNumber);
        IEnumerable<ScheduledProgramme> GetScheduledProgrammeListByDate(string date);
    }
}
