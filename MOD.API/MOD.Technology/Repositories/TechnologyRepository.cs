using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOD.TechnologyService.Contexts;
using MOD.TechnologyService.Models;

namespace MOD.TechnologyService.Repositories
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private readonly TechnologyContext _context;

        public TechnologyRepository(TechnologyContext context)
        {
            this._context = context;
        }

        public int AddTechnology(Technology technology)
        {
            try
            {
                _context.Technologies.Add(technology);
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteTechnology(string name)
        {
            try
            {
                var itemFound = _context.Technologies.FirstOrDefault(
                    tech =>
                        tech.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (itemFound != null)
                {
                    _context.Technologies.Remove(itemFound);
                    return _context.SaveChanges();
                }

                return -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Technology> GetTechnologies()
        {
            try
            {
                var itemsFound = _context.Technologies.ToList();
                return itemsFound;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Technology> GetTechnologiesByName(string name)
        {
            try
            {
                var itemsFound = _context.Technologies.Where(
                    tech => tech.Name.ToUpper().Contains(name.ToUpper()));
                return itemsFound;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateTechnology(Technology technology)
        {
            try
            {
                var storeTechnology = _context.Technologies.Attach(technology);
                storeTechnology.State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
