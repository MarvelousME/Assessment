using Phonebook.Database;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Phonebook.Repository
{
    public class PhoneBookTypeRepository : GenericRepository<PhoneBookType>, IPhoneBookTypeRepository
    {
        public PhoneBookTypeRepository(DbContext context)
            : base(context)
        {

        }

        public override IEnumerable<PhoneBookType> GetAll()
        {
            return _entities.Set<PhoneBookType>().AsEnumerable();
        }

        public PhoneBookType GetById(int id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<PhoneBookType> GetRecordsById(int id)
        {
            return _dbset.Where(x => x.Id == id).AsEnumerable();
        }
    }
}
