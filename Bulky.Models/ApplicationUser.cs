using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    public class ApplicationUser:IdentityUser  //Discriminator column in Sql Server: will basically have a value that will tell that the user on that record is that an applicationUser or is that an identityuser?
    {
        //we need to add that to migration and we also have to add a mapper for that ,we'll create DbSet. this is in dataaccess\data\ApplicationDbContext

        [Required]
        public int Name { get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
    }
}
