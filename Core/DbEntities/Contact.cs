using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DbEntities
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required]

        public string ContactName { get; set; }

        [Required]

        [ForeignKey("Company")]

        public int CompanyId { get; set; }

        [Required]

        [ForeignKey("Country")]

        public int CountryId { get; set; }

        public Company Company { get; set; }

        public Country Country { get; set; }
    }
}
