using Phonebook.Database.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phonebook.Database
{
    [Table("PhoneBookEntry")]
    public class PhoneBookEntry : AuditableEntity<int>
    {
        public int PhoneBookId { get; set; }

        [ForeignKey("PhoneBookId")]
        public virtual PhoneBook PhoneBook { get; set; }

        [Display(Name = "Number Type")]
        public int PhoneBookTypeId { get; set; }

        [ForeignKey("PhoneBookTypeId")]
        public virtual PhoneBookType PhoneBookType { get; set; }

        [Required]
        [MaxLength(10), MinLength(10)]
        public string Number { get; set; }

    }
}
