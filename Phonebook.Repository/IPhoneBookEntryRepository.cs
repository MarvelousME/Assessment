using Phonebook.Database;
using System.Collections.Generic;


namespace Phonebook.Repository
{
    public interface IPhoneBookEntryRepository : IGenericRepository<PhoneBookEntry>
    {
        IEnumerable<PhoneBookEntry> GetRecordsByPhoneBookId(int Id);

        PhoneBookEntry GetById(int id);
    }
}
