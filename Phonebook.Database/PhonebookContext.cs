using Phonebook.Database.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;

namespace Phonebook.Database
{

    public class PhonebookContext : DbContext
    {

        public PhonebookContext() : base("Name=PhonebookDBConnectionString")
        {
            System.Data.Entity.Database.SetInitializer(new PhonebookDBInitializer());
            Database.Initialize(true);
        }

        public DbSet<PhoneBook> PhoneBooks { get; set; }

        public DbSet<PhoneBookType> PhoneBookTypes { get; set; }

        public DbSet<PhoneBookEntry> PhoneBookEntries { get; set; }

        public override int SaveChanges()
        {
            try
            {
                var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => x.Entity is IAuditableEntity
                        && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

                foreach (var entry in modifiedEntries)
                {
                    IAuditableEntity entity = entry.Entity as IAuditableEntity;
                    if (entity != null)
                    {
                        string identityName = Thread.CurrentPrincipal.Identity.Name;
                        DateTime now = DateTime.UtcNow;

                        if (entry.State == System.Data.Entity.EntityState.Added)
                        {
                            entity.CreatedBy = identityName;
                            entity.CreatedDate = now;
                        }
                        else
                        {
                            base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                            base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        }

                        entity.UpdatedBy = identityName;
                        entity.UpdatedDate = now;
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }

                throw raise;
            }

            return base.SaveChanges();
        }

        private object HandleDbEntityValidationException(DbEntityValidationException vex)
        {
            throw new NotImplementedException();
        }

        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }
    }

    public class PhonebookDBInitializer : DropCreateDatabaseIfModelChanges<PhonebookContext>
    {
        protected override void Seed(PhonebookContext context)
        {
            var listPhoneBookTypes = new List<PhoneBookType>() {
             new PhoneBookType() { Id = 1, Description = "Home" },
             new PhoneBookType() { Id = 2, Description = "Work" },
             new PhoneBookType() { Id = 3, Description = "Cell" }
            };

            context.PhoneBookTypes.AddRange(listPhoneBookTypes);

            base.Seed(context);
        }
    }
}
