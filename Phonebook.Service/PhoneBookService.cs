using Phonebook.Database;
using Phonebook.Repository;

namespace Phonebook.Service
{
    public class PhoneBookService : EntityService<PhoneBook>, IPhoneBookService
    {
        IUnitOfWork _unitOfWork;
        IPhoneBookRepository _phonebookRepository;

        public PhoneBookService(IUnitOfWork unitOfWork, IPhoneBookRepository phoneBookRepository)
           : base(unitOfWork, phoneBookRepository)
        {
            _unitOfWork = unitOfWork;
            _phonebookRepository = phoneBookRepository;
        }

        public PhoneBook GetById(int Id)
        {
            return _phonebookRepository.GetById(Id);
        }

        public bool CheckIfContactExists(string Name, string Surname)
        {
            return _phonebookRepository.CheckIfContactExists(Name, Surname);
        }
    }
}
