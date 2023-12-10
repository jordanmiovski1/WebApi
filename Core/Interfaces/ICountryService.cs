using Core.DbEntities;
using Core.WebEntities.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetAllCountries();

        Task<CountryDTO> CreateCountry(CountryInsertDTO country);

        Task UpdateCountry(CountryDTO country);

        Task DeleteCountry(int id);
    }
}
