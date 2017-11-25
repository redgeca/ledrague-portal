using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeDragueCoreObjects.Contracts
{
    public class Artist
    {
        public Artist()
        {
            this.Contracts = new HashSet<Contract>();
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom de l'artiste est obligatoire")]
        public string ArtistName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string SSN { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
