using log4net;
using Phonebook.Database;
using Phonebook.Service;
using Phonebook.Web.Controllers.Base;
using Phonebook.Web.Dtos;
using Phonebook.Web.Hubs;
using Phonebook.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Phonebook.Web.Controllers
{
    public class PhonebookController : BaseController
    {
        IPhoneBookService _PhoneBookService;
        IPhoneBookEntryService _PhoneBookEntryService;
        IPhoneBookTypeService _PhoneBookTypeService;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PhonebookController(IPhoneBookService PhonebookService, IPhoneBookEntryService PhoneBookEntryService, IPhoneBookTypeService PhoneBookTypeService)
        {
            _PhoneBookService = PhonebookService;
            _PhoneBookEntryService = PhoneBookEntryService;
            _PhoneBookTypeService = PhoneBookTypeService;
        }

        public ActionResult Index()
        {
            return View(GetAllContacts());
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var contact = _PhoneBookService.GetById(id);

                if (contact == null)
                    return HttpNotFound();

                var numberList = (from pe in _PhoneBookEntryService.GetRecordsByPhoneBookId(contact.Id)
                                  join pt in _PhoneBookTypeService.GetAll() on pe.PhoneBookTypeId equals pt.Id
                                  select new PhoneNumberEditorViewModel()
                                  {
                                      Id = pe.Id,
                                      Description = pt.Description,
                                      PhoneBookTypeId = pe.PhoneBookTypeId.ToString(),
                                      Number = pe.Number
                                  }).ToList();

                var contactDetail = new PhonebookEditViewModel
                {
                    Id = contact.Id,
                    FirstName = contact.Name,
                    LastName = contact.Surname,
                    Numbers = numberList,
                    ddlPhoneTypes = PopulateNumberTypesDropDownList()
                };

                //Store In Session so can populate in Partial
                Session["ddlPhoneTypes"] = contactDetail.ddlPhoneTypes;

                return View("PhoneBookView", contactDetail);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Phonebook", "Edit"));
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var viewModel = new PhonebookDetailViewModel();
                var numbers = new List<PhoneNumberEditorViewModel>();

                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var contact = _PhoneBookService.GetById(id);


                numbers = (from pe in _PhoneBookEntryService.GetRecordsByPhoneBookId(contact.Id)
                           join pt in _PhoneBookTypeService.GetAll() on pe.PhoneBookTypeId equals pt.Id
                           select new PhoneNumberEditorViewModel()
                           {
                               Id = pe.Id,
                               Description = pt.Description,
                               PhoneBookTypeId = pe.PhoneBookTypeId.ToString(),
                               Number = pe.Number
                           }).ToList();

                viewModel = new PhonebookDetailViewModel()
                {
                    FirstName = contact.Name,
                    LastName = contact.Surname,
                    Numbers = numbers
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Phonebook", "Details"));
            }
        }

        public ActionResult Add(PhonebookEditViewModel phonebookEditViewModel)
        {
            var contact = new PhoneBook()
            {
                Name = phonebookEditViewModel.FirstName,
                Surname = phonebookEditViewModel.LastName,
                CreatedDate = DateTime.Now
            };

            _PhoneBookService.Create(contact);

            var Id = contact.Id;

            if (Id == 0)
            {
                log.Error("Contact not saved. Please try again later!");
                Danger("Contact not saved. Please try again later!");
                return View("PhoneBookView", phonebookEditViewModel);

            }

            if (phonebookEditViewModel.Numbers.Count() > 0)
            {
                foreach (var entry in phonebookEditViewModel.Numbers)
                {
                    var phonebookEntry = new PhoneBookEntry()
                    {
                        PhoneBookId = Id,
                        PhoneBookTypeId = Convert.ToInt32(entry.PhoneBookTypeId),
                        Number = entry.Number
                    };

                    _PhoneBookEntryService.Create(phonebookEntry);
                    Id = phonebookEntry.Id;

                    if (Id == 0)
                    {
                        log.Error($"Contact number {entry.Number} not saved {phonebookEditViewModel.FirstName}!");

                        Danger($"Contact number {entry.Number} not saved {phonebookEditViewModel.FirstName}!");

                        return View("PhoneBookView", phonebookEditViewModel);
                    }
                }
            }

            Success(string.Format("<b>{0}</b> was successfully saved to the Phonebook.", phonebookEditViewModel.FirstName), true);

            PhonebookHub.BroadcastData();

            return RedirectToAction("Index", "Phonebook");
        }

        public ActionResult Update(PhonebookEditViewModel phonebookEditViewModel)
        {
            if (phonebookEditViewModel.Numbers.Count() > 0)
            {
                foreach (var entry in phonebookEditViewModel.Numbers)
                {
                    var record = _PhoneBookEntryService.GetById(entry.Id);

                    record.Number = entry.Number;
                    record.PhoneBookTypeId = Convert.ToInt32(entry.PhoneBookTypeId);

                    _PhoneBookEntryService.Update(record);
                }
            }

            Success(string.Format("<b>{0}</b> was successfully updated.", phonebookEditViewModel.FirstName), true);

            PhonebookHub.BroadcastData();

            return RedirectToAction("Index", "Phonebook");
        }

        public ActionResult Save()
        {
            return View("PhoneBookView", new PhonebookEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(PhonebookEditViewModel phonebookEditViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isUpdate = phonebookEditViewModel.Id != 0;


                    if (CheckIfContactAddedANumber(phonebookEditViewModel))
                    {
                        if (isUpdate)
                        {
                            return Update(phonebookEditViewModel);
                        }
                        else
                        {
                            if (!_PhoneBookService.CheckIfContactExists(phonebookEditViewModel.FirstName, phonebookEditViewModel.LastName))
                            {
                                return Add(phonebookEditViewModel);
                            }
                            else
                            {
                                Danger("Contact with the same name already exists.");
                                return View("PhoneBookView", phonebookEditViewModel);
                            }
                        }
                    }
                    else
                    {
                        Danger("You have to add atleast 1 number to the phonebook entry");
                        return View("PhoneBookView", phonebookEditViewModel);
                    }
                }
                else
                {
                    Danger("Looks like something went wrong. Model is Invalid.");
                    return View("PhoneBookView", phonebookEditViewModel);
                    
                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Phonebook", "Save"));
            }
        }

        public ActionResult AddNumber()
        {
            return PartialView("Editors/_PhoneBookNumberEditor", new PhoneNumberEditorViewModel(PopulateNumberTypesDropDownList()) { Numbers = PopulateNumberTypesDropDownList() });
        }

        public List<PhonebookDto> GetAllContacts()
        {
            List<PhonebookDto> contacts = new List<PhonebookDto>();

            var anyRecords = _PhoneBookService.GetAll().Count() != 0;

            if (anyRecords)
            {
                contacts = (from c in _PhoneBookService.GetAll()
                            select new PhonebookDto
                            {
                                Id = Convert.ToInt32(c.Id),
                                Name = c.Name,
                                Surname = c.Surname,
                                CreatedDate = c.CreatedDate
                            }).ToList();
            }

            return contacts;
        }

        public List<SelectListItem> PopulateNumberTypesDropDownList()
        {
            var listOfNumberTypes = new List<SelectListItem>();
            try
            {
                return listOfNumberTypes = _PhoneBookTypeService.GetAll().ToList().Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Description,
                }).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Failed to retrieve records from PhoneBookType table", ex);
            }

            ViewBag.NumberTypes = listOfNumberTypes;

            return listOfNumberTypes;
        }

        [System.Web.Http.HttpDelete]
        public ActionResult Delete(int id)
        {
            var contact = _PhoneBookService.GetById(id);

            try
            {
                _PhoneBookService.Delete(contact);

                var records = _PhoneBookEntryService.GetRecordsByPhoneBookId(contact.Id).ToList();

                if (records != null && records.Count() > 0)
                {
                    foreach (var record in records)
                    {
                        _PhoneBookEntryService.Delete(record);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"Contact {contact.Name} {contact.Surname} was not deleted, something went wrong!", ex);

                Danger($"Contact {contact.Name} {contact.Surname} was not deleted, something went wrong!", true);

                return View(contact);
            }

            Success(string.Format("<b>{0}</b> was successfully deleted.", contact.Name), true);

            PhonebookHub.BroadcastData();

            return RedirectToAction("Index", "Phonebook");
        }

        public bool CheckIfContactAddedANumber(PhonebookEditViewModel phonebookEditViewModel)
        {
            return (phonebookEditViewModel.Numbers != null && phonebookEditViewModel.Numbers.Count() > 0);
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            Exception exception = filterContext.Exception;

            filterContext.ExceptionHandled = true;


            var Result = this.View("Error", new HandleErrorInfo(exception,
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString()));

            filterContext.Result = Result;

            log.Error(filterContext.Exception);

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
        }
    }
}