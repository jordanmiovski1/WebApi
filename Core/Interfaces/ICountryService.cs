using Core.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country> CreateCountry(Country country);
        Task UpdateCountry(int id, Country country);
        Task DeleteCountry(int id);
    }
}
