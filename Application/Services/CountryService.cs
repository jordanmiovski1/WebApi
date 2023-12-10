using Core.DbEntities;
using Core.Interfaces;
using Core.WebEntities.Company;
using Core.WebEntities.Country;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly WebAppContext _context;

        public CountryService(WebAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CountryDTO>> GetAllCountries()
        {
            return MapToModify(await _context.Country.ToListAsync());
        }

        public async Task<CountryDTO> CreateCountry(CountryInsertDTO country)
        {
            var countryToInsert = new Country() { CountryName = country.CountryName };
            _context.Country.Add(countryToInsert);
            await _context.SaveChangesAsync();
            return MapToModify(countryToInsert);
        }

        public async Task UpdateCountry(CountryDTO country)
        {
            try
            {
                var countryDb = await _context.Country.FindAsync(country.Id);
                if (countryDb == null)
                    throw new Exception("Record not found!");

                countryDb.CountryName = country.CountryName;

                _context.Entry(countryDb).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
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

        #region Helpers

        private IEnumerable<CountryDTO> MapToModify(List<Country> companies)
        {
            return companies.Select(x => new CountryDTO()
            {
                Id = x.CountryId,
                CountryName = x.CountryName
            });

        }

        private CountryDTO MapToModify(Country? country)
        {
            if (country == null)
                throw new Exception("Record not found!");

            return new CountryDTO() { Id = country.CountryId, CountryName = country.CountryName };

        }

        #endregion
    }
}
