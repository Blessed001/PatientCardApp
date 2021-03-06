﻿using PatientCardApp.DataAccess;
using PatientCardApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PatientCardApp.UI.Data.Lookups
{
    public class LookUpDataService : IPatientCardLookUpDataService, IGenderLookUpDataService
    {
        private readonly Func<PatientCardContext> _contextCreator;

        public LookUpDataService(Func<PatientCardContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookUpItem>> GetPatientCardLookUpAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.PatientCards.AsNoTracking()
                    .Select(pc => new LookUpItem
                    {
                        Id = pc.Id,
                        DisplayMember = pc.LastName
                    }).ToListAsync();
            }
        }

        public async Task<IEnumerable<LookUpItem>> GetGenderLookUpAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Genders.AsNoTracking()
                    .Select(g => new LookUpItem
                    {
                        Id = g.Id,
                        DisplayMember = g.Name
                    }).ToListAsync();
            }
        }
    }
}
