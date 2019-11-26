using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Phonebook.Web.ViewModels
{
    public class PhoneNumberEditorViewModel
    {
        public PhoneNumberEditorViewModel()
        {
            Numbers = new List<SelectListItem>();
        }

        public PhoneNumberEditorViewModel(List<SelectListItem> dropdownlistItems)
        {
            Numbers = dropdownlistItems;
        }

        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [StringLength(10, ErrorMessage = "Contact Number - 10 digits only allowed", MinimumLength = 10)]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Number Type")]
        public string PhoneBookTypeId { get; set; }
        public List<SelectListItem> Numbers { get; set; }
    }
}