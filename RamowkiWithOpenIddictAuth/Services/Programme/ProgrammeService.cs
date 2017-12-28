using System.Collections.Generic;
using System.Linq;

using RamowkiWithOpenIddictAuth.DTO;
using RamowkiWithOpenIddictAuth.Models;

namespace RamowkiWithOpenIddictAuth.Services.ProgrammeService
{
    public class ProgrammeService : IProgrammeService
    {
        private ApplicationDbContext dbContext;

        public IEnumerable<Programme> ReadProgrammeList()
        {
            return this.dbContext.Programmes.ToList();
        }

        public Programme ReadProgramme(string id)
        {
            return this.dbContext.Programmes.Single(p => p.Id.ToString() == id);
        }

        public void CreateProgramme(ProgrammeJson programme)
        {
            CreateProgrammeAndReturnId(programme);
        }

        public string CreateProgrammeAndReturnId(ProgrammeJson programme)
        {
            Programme newProgramme = new Programme(programme.Title, programme.Description);
            this.dbContext.Programmes.Add(newProgramme);
            this.dbContext.SaveChanges();
            return newProgramme.Id.ToString();
        }

        public void UpdateProgramme(string id, ProgrammeJson programme)
        {
            Programme programmeToEdit = this.ReadProgramme(id);
            programmeToEdit.Title = programme.Title;
            programmeToEdit.Description = programme.Description;
            this.dbContext.SaveChanges();
        }

        public void DeleteProgramme(string id)
        {
            this.dbContext.Remove(this.ReadProgramme(id));
            this.dbContext.SaveChanges();
        }

        public ProgrammeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
