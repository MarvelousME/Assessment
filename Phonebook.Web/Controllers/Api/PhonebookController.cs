using log4net;
using Phonebook.Service;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Phonebook.Web.Controllers.Api
{
    public class PhonebookController : ApiController
    {

        IPhoneBookService _PhoneBookService;
        IPhoneBookEntryService _PhoneBookEntryService;
        IPhoneBookTypeService _PhoneBookTypeService;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PhonebookController() { }

        public PhonebookController(IPhoneBookService PhonebookService, IPhoneBookEntryService PhoneBookEntryService, IPhoneBookTypeService PhoneBookTypeService)
        {
            _PhoneBookService = PhonebookService;
            _PhoneBookEntryService = PhoneBookEntryService;
            _PhoneBookTypeService = PhoneBookTypeService;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var contact = _PhoneBookService.GetById(id);

            if (contact == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

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
                log.Error($"Contact {contact.Name} {contact.Surname} was not deleted, something went wrong!");
            }
        }
    }
}
