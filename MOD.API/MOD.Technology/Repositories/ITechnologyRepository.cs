using MOD.TechnologyService.Models;
using System.Collections.Generic;

namespace MOD.TechnologyService.Repositories
{
    public interface ITechnologyRepository
    {
        IEnumerable<Technology> GetTechnologies();
        IEnumerable<Technology> GetTechnologiesByName(string name);
        int AddTechnology(Technology technology);
        int UpdateTechnology(Technology technology);
        int DeleteTechnology(string name);
    }
}
