using Core.DbEntities;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CountryService: ICountryService
    {
        private readonly WebAppContext _context;

        public CountryService(WebAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _context.Country.ToListAsync();
        }

        public async Task<Country> CreateCountry(Country country)
        {
            _context.Country.Add(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task UpdateCountry(int id, Country country)
        {
            if (id != country.CountryId)
            {
                throw new Exception("Id incorect");
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Country.Any(e => e.CountryId == id))
                {
                    throw new Exception("Record not found!");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteCountry(int id)
        {
            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                throw new Exception("Record not found!");
            }

            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
        }
    }
}
