using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using RamowkiWithOpenIddictAuth.DTO;
using RamowkiWithOpenIddictAuth.Services.ScheduleService;
using RamowkiWithOpenIddictAuth.Models;

namespace RamowkiWithOpenIddictAuth.Controllers
{
    [Route("api/[controller]")]
    public class ScheduleController : Controller
    {

        private readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void CreateSchedule([FromBody]ScheduledProgrammeWrapper schedule, [FromQuery] String programme = null)
        {
            if( programme != null )
            {
                this.scheduleService.CreateSchedule(schedule.ToScheduledProgrammeJson(), programme);
            }
            else
            {
                this.scheduleService.CreateScheduleAndProgramme(schedule.ToScheduledProgrammeJson(), schedule.Programme);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{scheduleID}")]
        public void UpdateSchedule(string scheduleID, [FromBody] ScheduledProgrammeJson programme, [FromQuery(Name = "programme")] String programmeID = null)
        {
            if( ( programmeID == null ) && ( programme != null ) )
            {
                this.scheduleService.UpdateSchedule(scheduleID, programme);
            }
            else if( ( programmeID != null ) && ( programme == null ) )
            {
                this.scheduleService.SwitchProgramme(scheduleID, programmeID);
            }
            /*
            else
            {
                return BadRequest(); // na razie nie działa, bo cholerstwo musiałoby zwracać określone typy
            }
            */
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{scheduleID}")] 
        public void DeleteSchedule(string scheduleID)
        {
            this.scheduleService.DeleteSchedule(scheduleID);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{scheduleID}")]
        public ScheduledProgramme GetScheduledProgramme(string scheduleID)
        {
            return this.scheduleService.GetScheduledProgramme(scheduleID);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IEnumerable<ScheduledProgramme> GetScheduledProgrammeList([FromQuery(Name = "day")] int? dayNumber = null, [FromQuery] String date = null)
        {
            if (date != null)
            {
                return this.scheduleService.GetScheduledProgrammeListByDate(date);
            }
            else if (dayNumber != null)
            {
                return this.scheduleService.GetScheduledProgrammeListByWeekDay((int)dayNumber);
            }
            else return null;
        }
    }
}
