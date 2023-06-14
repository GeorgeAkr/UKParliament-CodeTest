using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.MockData.Repositories;
using Xunit;

namespace UKParliament.CodeTest.Tests
{
    public class PersonRepositoryMockTests
    {
        private readonly PersonRepository repository;

        public PersonRepositoryMockTests()
        {
            repository = new PersonRepository();
        }

        [Fact]
        public async Task GetAll()
        {
            // Arrange
            var expected = 1;

            // Act
            var persons = await repository.GetAllAsync();
            var actual = persons.Count;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            var id = 1;
            var expected = id;

            // Act
            var person = await repository.GetByIdAsync(id);
            var actual = person.Id;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Add()
        {
            // Arrange
            var id = 1;
            var personInput = new Person();
            var expected = id;

            // Act
            var person = await repository.AddAsync(personInput);
            var actual = person.Id;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var person = new Person();
            var expected = true;

            // Act
            var actual = await repository.UpdateAsync(person);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Delete()
        {
            // Arrange
            var id = 1;
            var expected = true;

            // Act
            var actual = await repository.DeleteAsync(id);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}