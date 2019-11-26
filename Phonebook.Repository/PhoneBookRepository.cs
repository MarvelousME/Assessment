using Phonebook.Database;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Phonebook.Repository
{
    public class PhoneBookRepository : GenericRepository<PhoneBook>, IPhoneBookRepository
    {
        DbContext _context;
        public PhoneBookRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }

        public override IEnumerable<PhoneBook> GetAll()
        {
            return _entities.Set<PhoneBook>().AsEnumerable();
        }

        public PhoneBook GetById(int id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Save(PhoneBook phoneBook)
        {
            _dbset.Add(phoneBook);
        }

        public bool CheckIfContactExists(string Name, string Surname)
        {
            var person = $"{Name.ToLower().Trim()} {Surname.ToLower().Trim()}";

            var checkInDb = (from p in GetAll()
                             where p.Name.ToLower().Trim() == Name.ToLower().Trim()
                             && p.Surname.ToLower().Trim() == Surname.ToLower().Trim()
                             select p).ToList();

            return checkInDb.Count > 0;

        }
    }
}
