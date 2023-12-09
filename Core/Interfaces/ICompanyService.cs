using Core.DbEntities;
using Core.WebEntities.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetAllCompanies();
        Task<CompanyDTO> GetCompany(int id);
        Task<CompanyDTO> CreateCompany(CompanyInsertDTO companyDTO);
        Task UpdateCompany(CompanyDTO company);
        Task DeleteCompany(int id);

    }
}
