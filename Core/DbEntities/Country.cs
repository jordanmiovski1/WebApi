using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DbEntities
{
    public class Country
    {
        public int CountryId { get; set; }
        [Required]

        public string CountryName { get; set; }

        private ICollection<Contact> Contacts { get; set; }
    }
}
