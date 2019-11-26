using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Web.ViewModels
{
    public class PhonebookDetailViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Contact Numbers")]
        public List<PhoneNumberEditorViewModel> Numbers { get; set; }

    }
}