using Phonebook.Database;

namespace Phonebook.Service
{
    public interface IPhoneBookService : IEntityService<PhoneBook>
    {
        PhoneBook GetById(int Id);

        bool CheckIfContactExists(string Name, string Surname);
    }
}
