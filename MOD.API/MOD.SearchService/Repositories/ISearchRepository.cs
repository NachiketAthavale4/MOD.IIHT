using MOD.SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.SearchService.Repositories
{
    public interface ISearchRepository
    {
        IEnumerable<Training> GetTrainingsByTechnology(string technology);
    }
}
