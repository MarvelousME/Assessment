using Phonebook.Database;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Phonebook.Repository
{
    public class PhoneBookEntryRepository : GenericRepository<PhoneBookEntry>, IPhoneBookEntryRepository
    {
        public PhoneBookEntryRepository(DbContext context)
            : base(context)
        {

        }

        public override IEnumerable<PhoneBookEntry> GetAll()
        {
            return _entities.Set<PhoneBookEntry>().Include(p => p.PhoneBook).Include(pt => pt.PhoneBookType).AsEnumerable();
        }

        public PhoneBookEntry GetById(int id)
        {
            return _dbset.Include(p => p.PhoneBook).Include(pt => pt.PhoneBookType).Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<PhoneBookEntry> GetRecordsByPhoneBookId(int id)
        {
            return _dbset.Include(p => p.PhoneBook).Include(pt => pt.PhoneBookType).Where(x => x.PhoneBookId == id).AsEnumerable();
        }
    }
}
