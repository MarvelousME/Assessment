using Phonebook.Database;
using Phonebook.Repository;

namespace Phonebook.Service
{
    public class PhoneBookTypeService : EntityService<PhoneBookType>, IPhoneBookTypeService
    {
        IUnitOfWork _unitOfWork;
        IPhoneBookTypeRepository _phonebookTypeRepository;

        public PhoneBookTypeService(IUnitOfWork unitOfWork, IPhoneBookTypeRepository phonebookTypeRepository)
          : base(unitOfWork, phonebookTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _phonebookTypeRepository = phonebookTypeRepository;

        }
    }
}
