using System;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Web.Dtos
{
    public class PhonebookDto
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}