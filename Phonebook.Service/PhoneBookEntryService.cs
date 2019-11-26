using Phonebook.Database;
using Phonebook.Repository;
using System.Collections.Generic;

namespace Phonebook.Service
{
    public class PhoneBookEntryService : EntityService<PhoneBookEntry>, IPhoneBookEntryService
    {
        IUnitOfWork _unitOfWork;
        IPhoneBookEntryRepository _phonebookEntryRepository;

        public PhoneBookEntryService(IUnitOfWork unitOfWork, IPhoneBookEntryRepository phonebookEntryRepository)
          : base(unitOfWork, phonebookEntryRepository)
        {
            _unitOfWork = unitOfWork;
            _phonebookEntryRepository = phonebookEntryRepository;
        }

        public PhoneBookEntry GetById(int Id)
        {
            return _phonebookEntryRepository.GetById(Id);
        }

        public IEnumerable<PhoneBookEntry> GetRecordsByPhoneBookId(int Id)
        {
            return _phonebookEntryRepository.GetRecordsByPhoneBookId(Id);
        }
    }
}
