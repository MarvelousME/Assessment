using Phonebook.Database;
using System.Collections.Generic;

namespace Phonebook.Service
{
    public interface IPhoneBookEntryService : IEntityService<PhoneBookEntry>
    {
        PhoneBookEntry GetById(int Id);

        IEnumerable<PhoneBookEntry> GetRecordsByPhoneBookId(int Id);
    }
}
