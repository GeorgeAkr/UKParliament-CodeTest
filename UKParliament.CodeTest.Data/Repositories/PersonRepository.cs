using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace UKParliament.CodeTest.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonManagerContext context;

        public PersonRepository(PersonManagerContext context)
        {
            this.context = context;
        }

        public async Task<Person?> AddAsync(Person newPerson, CancellationToken ct = default)
        {
            context.People.Add(newPerson);
            return await context.SaveChangesAsync(ct) == 1 ? newPerson : null;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            bool success = false;
            Person toBeDeleted = context.People.Find(id);
            if (toBeDeleted != null)
            {
                context.People.Remove(toBeDeleted);
                await context.SaveChangesAsync(ct);
                success = true;
            }
            return success;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<List<Person>> GetAllAsync(CancellationToken ct = default)
        {
            return await context.People.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Person> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await context.People.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Person car, CancellationToken ct = default)
        {
            bool success = false;
            Person toBeUpdated = context.People.Find(car.Id);
            if (toBeUpdated != null)
            {
                context.People.Update(toBeUpdated);
                await context.SaveChangesAsync(ct);
                success = true;
            }
            return success;
        }
    }
}
