using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using eventy.Data;
using eventy.Models;
using System.Threading.Tasks;

namespace eventy.Repositories
{
    public class FamiliesRepository
    {
        public EventyDbContext EventyDbContext { get; }

        public FamiliesRepository(EventyDbContext eventyDbContext)
        {
            EventyDbContext = eventyDbContext;
        }

        public async Task<List<Family>> GetAllFamilies()
        {
            using (var context = EventyDbContext)
            {
                return await context.Families
                    .ToListAsync();
            }
        }

        public async Task<List<Family>> FindAllFamiliesByName(string name)
        {
            using (var context = EventyDbContext)
            {
                return await context.Families
                    .Where(f => f.Name.ToUpper() == name.ToUpper())
                    .ToListAsync();
            }
        }

        public async Task<List<Family>> FindAllFamiliesByOldFamilyNumber(string oldFamilyNumber)
        {
            using(var context = EventyDbContext)
            {
                return await context.Families
                    .Where(f => f.OldFamilyNumber.ToUpper() == oldFamilyNumber.ToUpper())
                    .ToListAsync();
            }
        }
    }
}