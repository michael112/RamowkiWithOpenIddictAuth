using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using RamowkiWithOpenIddictAuth.Services.ProgrammeService;
using RamowkiWithOpenIddictAuth.Models;
using RamowkiWithOpenIddictAuth.DTO;


namespace RamowkiWithOpenIddictAuth.Controllers
{
    [Route("api/[controller]")]
    public class ProgrammeController : Controller
    {

        private readonly IProgrammeService programmeService;

        public ProgrammeController(IProgrammeService programmeService)
        {
            this.programmeService = programmeService;
        }

        // GET api/programme
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IEnumerable<Programme> ReadProgrammeList()
        {
            return this.programmeService.ReadProgrammeList();
        }

        // GET api/programme/{programmeID}
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{programmeID}")]
        public Programme ReadProgramme(string programmeID)
        {
            return this.programmeService.ReadProgramme(programmeID);
        }

        // POST api/programme
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void CreateProgramme([FromBody]ProgrammeJson newProgramme)
        {
            this.programmeService.CreateProgramme(newProgramme);
        }

        // PUT api/programme/{programmeID}
        [Authorize(Roles = "Admin")]
        [HttpPut("{programmeID}")]
        public void UpdateProgramme(string programmeID, [FromBody]ProgrammeJson programme)
        {
            this.programmeService.UpdateProgramme(programmeID, programme);
        }

        // DELETE api/programmeID/{programmeID}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{programmeID}")]
        public void DeleteProgramme(string programmeID)
        {
            this.programmeService.DeleteProgramme(programmeID);
        }
    }
}
