using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.MockData.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Task<Person> AddAsync(Person newPerson, CancellationToken ct = default)
        {
            newPerson.Id = 1;
            return Task.FromResult(newPerson);
        }

        public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            return Task.FromResult(true);
        }

        public Task<List<Person>> GetAllAsync(CancellationToken ct = default)
        {
            List<Person> personList = new()
            {
                new Person { Id = 1, Name = "Name_a", Address = "address_a", Details = "details_a", DateOfBirth = new DateTime(2000, 12, 20) }
            };
            return Task.FromResult(personList);
        }

        public Task<Person> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return Task.FromResult(new Person { Id = 1, Name = "Name_a", Address = "address_a", Details = "details_a", DateOfBirth = new DateTime(2000, 12, 20) });
        }

        public Task<bool> UpdateAsync(Person person, CancellationToken ct = default)
        {
            return Task.FromResult(true);
        }
    }
}
