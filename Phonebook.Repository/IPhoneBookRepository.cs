using System;
using Phonebook.Database;


namespace Phonebook.Repository
{
    public interface IPhoneBookRepository : IGenericRepository<PhoneBook>
    {
        PhoneBook GetById(int id);

        bool CheckIfContactExists(string Name, string Surname);
    }
}
