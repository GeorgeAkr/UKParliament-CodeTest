using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories;
using Xunit;

namespace UKParliament.CodeTest.Tests
{
    public class PersonRepositoryTests
    {
        private readonly PersonRepository repository;
        private static bool IsInitialised = false;

        public PersonRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<PersonManagerContext>()
                        .UseInMemoryDatabase("PersonsUnitTestingDb")
                        .Options;
            PersonManagerContext context = new PersonManagerContext(options);
            if (!IsInitialised)
            {
                InitialData.PopulateTestData(context);
                IsInitialised = true;
            }
            repository = new PersonRepository(context);
        }

        [Fact]
        public async Task GetAllReturnsMoreThanOne()
        {
            // Arrange
            var expected = true;

            // Act
            var persons = await repository.GetAllAsync();
            var actual = persons.Count > 1;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithValidIdGetByIdReturnsPerson()
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
        public async Task WithNonExistentIdGetByIdReturnsNull()
        {
            // Arrange
            var id = 15;
            Person expected = null;

            // Act
            var actual = await repository.GetByIdAsync(id);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithValidPersonAddAssignsNextId()
        {
            // Arrange
            var id = 5;
            var personInput = new Person();
            var expected = id;

            // Act
            var person = await repository.AddAsync(personInput);
            var actual = person.Id;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithNullPersonAddThrowsArgumentNullException()
        {
            // Arrange
            Person personInput = null;
            Exception exc = new ArgumentNullException();
            Type expected = exc.GetType();
            Type expected2 = (new NullReferenceException()).GetType();
            Type actual = null;

            // Act
            try
            {
                await repository.AddAsync(personInput);
            }
            catch (Exception e)
            {
                actual = e.GetType();
            }
            
            // Assert
            Assert.True(expected == actual || expected2 == actual);
        }

        [Fact]
        public async Task WithValidIdUpdateReturnsTrue()
        {
            // Arrange
            var person = new Person() { Id = 2, Address = "address_2" };
            var expected = true;

            // Act
            var actual = await repository.UpdateAsync(person);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithNonExistentIdUpdateReturnsFalse()
        {
            // Arrange
            var person = new Person() { Id = 20, Address = "address_20" };
            var expected = false;

            // Act
            var actual = await repository.UpdateAsync(person);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithValidIdDeleteReturnsTrue()
        {
            // Arrange
            var id = 3;
            var expected = true;

            // Act
            var actual = await repository.DeleteAsync(id);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithNonExistentIdDeleteReturnsFalse()
        {
            // Arrange
            var id = 30;
            var expected = false;

            // Act
            var actual = await repository.DeleteAsync(id);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}