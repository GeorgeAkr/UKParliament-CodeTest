using Microsoft.Extensions.Caching.Memory;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories;

namespace UKParliament.CodeTest.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;
        private readonly IMemoryCache cache;
        private const int CachingExpirationSeconds = 60;

        public PersonService(IPersonRepository personRepository, IMemoryCache cache)
        {
            this.personRepository = personRepository;
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<Person> AddPersonAsync(Person person, CancellationToken ct = default)
        {
            var addedPerson = await personRepository.AddAsync(person, ct);
            UpdateCache(addedPerson, addedPerson is not null);
            return addedPerson;
        }

        public Task<bool> DeletePersonAsync(int id, CancellationToken ct = default)
        {
            var result = personRepository.DeleteAsync(id, ct);
            if (result.Result) cache.Remove(id);
            return result;
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync(CancellationToken ct = default)
        {
            return await personRepository.GetAllAsync(ct);
        }

        public async Task<Person> GetPersonByIdAsync(int id, CancellationToken ct = default)
        {
            if(!cache.TryGetValue(id, out Person person))
            {
                person = await personRepository.GetByIdAsync(id, ct);
                UpdateCache(person, person is not null);
            }
            return person;
        }

        public async Task<bool> UpdatePersonAsync(Person model, CancellationToken ct = default)
        {
            var person = await personRepository.GetByIdAsync(model.Id, ct);

            if (person is null)
            {
                return false;
            }

            person.Name = model.Name ?? person.Name;
            person.Address = model.Address ?? person.Address;
            person.Details = model.Details ?? person.Details;
            person.DateOfBirth = model.DateOfBirth == DateTime.MinValue ? person.DateOfBirth : model.DateOfBirth;

            var result = await personRepository.UpdateAsync(person, ct);
            UpdateCache(person, result);

            return result;
        }

        private void UpdateCache(Person person, bool performUpdate = true)
        {
            if (!performUpdate) return;

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(CachingExpirationSeconds));
            cache.Set(person.Id, person, cacheEntryOptions);
        }
    }
}
