using Phonebook.Database.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Database
{
    [Table("PhoneBook")]
    public class PhoneBook : AuditableEntity<int>
    {

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Surname { get; set; }

    }
}
