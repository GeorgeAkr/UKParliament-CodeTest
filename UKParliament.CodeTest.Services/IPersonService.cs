using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync(CancellationToken ct = default);
        Task<Person> GetPersonByIdAsync(int id, CancellationToken ct = default);
        Task<Person> AddPersonAsync(Person newPerson, CancellationToken ct = default);
        Task<bool> UpdatePersonAsync(Person person, CancellationToken ct = default);
        Task<bool> DeletePersonAsync(int id, CancellationToken ct = default);
    }
}