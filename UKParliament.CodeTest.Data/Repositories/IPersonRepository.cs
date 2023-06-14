namespace UKParliament.CodeTest.Data.Repositories
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync(CancellationToken ct = default);
        Task<Person> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Person?> AddAsync(Person newPerson, CancellationToken ct = default);
        Task<bool> UpdateAsync(Person Person, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
