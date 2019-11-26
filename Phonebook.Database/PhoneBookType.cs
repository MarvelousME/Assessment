using Phonebook.Database.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Database
{
    [Table("PhoneBookType")]
    public class PhoneBookType : AuditableEntity<int>
    {

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
