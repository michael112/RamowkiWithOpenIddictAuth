using System.Collections.Generic;

using RamowkiWithOpenIddictAuth.DTO;

namespace RamowkiWithOpenIddictAuth.Services.ProgrammeService
{
    public interface IProgrammeService
    {
        IEnumerable<RamowkiWithOpenIddictAuth.Models.Programme> ReadProgrammeList();
        RamowkiWithOpenIddictAuth.Models.Programme ReadProgramme(string id);
        void CreateProgramme(ProgrammeJson programme);
        string CreateProgrammeAndReturnId(ProgrammeJson programme);
        void UpdateProgramme(string id, ProgrammeJson programme);
        void DeleteProgramme(string id);
    }
}
