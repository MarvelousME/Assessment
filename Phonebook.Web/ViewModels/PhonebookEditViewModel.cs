using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Phonebook.Web.ViewModels
{
    public class PhonebookEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public List<SelectListItem> ddlPhoneTypes { get; set; }

        [Display(Name = "Contact Numbers")]
        public List<PhoneNumberEditorViewModel> Numbers { get; set; }
    }
}