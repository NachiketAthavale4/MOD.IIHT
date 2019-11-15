using MOD.SearchService.Contexts;
using MOD.SearchService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOD.SearchService.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly SearchContext searchContext;

        public SearchRepository(SearchContext searchContext)
        {
            this.searchContext = searchContext;
        }

        public IEnumerable<Training> GetTrainingsByTechnology(string technology)
        {
            try
            {
                var resultSet = from tech in searchContext.Technologies
                                where tech.Name.ToUpper().Contains(
                                    technology.ToUpper())
                                join train in searchContext.Trainings
                                on tech.Id equals train.TechnologyId
                                select train;
                return resultSet.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
