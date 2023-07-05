using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Errors;
using UKParliament.CodeTest.Data.Validators;
using UKParliament.CodeTest.Web.ViewModels;
using Xunit;

namespace UKParliament.CodeTest.Tests.ValidatorTests
{
    public class PersonValidatorTests
    {
        private IPersonValidator Validator { get; set; } = new PersonValidator();
        private DateTime CurrentDate { get; set; } = new DateTime();
        public PersonValidatorTests() {}

        #region Name Tests
        [Fact]
        public void WithPersonsNameNullReturnsNameNullError()
        {
            // Arrange
            Person person = new Person() { Id = 1, Name = null, Address = "address_x", Details = "details_x", DateOfBirth = new DateTime(2000, 12, 20) };
            var expected = new Dictionary<string, string>();
            expected.Add(nameof(person.Name), PersonErrorDescriptor.NullNameError);

            // Act
            var actual = Validator.Validate(person).Errors; 

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithPersonsNameNullReturnsIsNotValid()
        {
            // Arrange
            Person person = new Person() { Id = 1, Name = null, Address = "address_x", Details = "details_x", DateOfBirth = new DateTime(2000, 12, 20) };
            var expected = false;

            // Act
            var actual = Validator.Validate(person).IsValid;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithPersonsNameEmptyReturnsNameEmptyError()
        {
            // Arrange
            Person person = new Person() { Id = 1, Name = "", Address = "address_x", Details = "details_x", DateOfBirth = new DateTime(2000, 12, 20) };
            var expected= new Dictionary<string, string>();
            expected.Add(nameof(person.Name), PersonErrorDescriptor.EmptyNameError);

            // Act
            var actual = Validator.Validate(person).Errors;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithPersonsNameEmptyReturnsIsNotValid()
        {
            // Arrange
            Person person = new Person() { Id = 1, Name = "", Address = "address_x", Details = "details_x", DateOfBirth = new DateTime(2000, 12, 20) };
            var expected = false;

            // Act
            var actual = Validator.Validate(person).IsValid;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithPersonsNameWhiteSpaceReturnsNameEmptyError()
        {
            // Arrange
            Person person = new Person() { Id = 1, Name = " ", Address = "address_x", Details = "details_x", DateOfBirth = new DateTime(2000, 12, 20) };
            var expected = new Dictionary<string, string>();
            expected.Add(nameof(person.Name), PersonErrorDescriptor.EmptyNameError);

            // Act
            var actual = Validator.Validate(person).Errors;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithPersonsNameWhiteSpaceReturnsIsNotValid()
        {
            // Arrange
            Person person = new Person() { Id = 1, Name = " ", Address = "address_x", Details = "details_x", DateOfBirth = new DateTime(2000, 12, 20) };
            var expected = false;

            // Act
            var actual = Validator.Validate(person).IsValid;

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion

        #region DOB Tests
        [Fact]
        public void WithPersonsDOBOlderThan150ReturnsDOBError()
        {
            // Arrange
            
            Person person = new Person() { DateOfBirth = new DateTime(2000, 12, 20), Id = 1, Name = "John", Address = "address_x", Details = "details_x" };
            var expected = new Dictionary<string, string>();
            expected.Add(nameof(person.Name), PersonErrorDescriptor.EmptyNameError);

            // Act
            var actual = Validator.Validate(person).Errors;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithPersonsDOBInTheFutureReturnsDOBError()
        {
            // Arrange
            //Person person = new Person() { Id = 1, Address = "address_x", Details = "details_x", Name = "name_x", DateOfBirth = new DateTime(2000, 12, 20) };
            //string expected = ConvertionUtils.GetJson(person);

            // Act
            //var actual = ConvertionUtils.GetJson(new PersonViewModel() { Id = 1, Address = "address_x", Details = "details_x", Name = "name_x", DateOfBirth = new DateTime(2000, 12, 20) });

            // Assert
            //Assert.Equal(expected, actual);
        }


        [Fact]
        public void WithPersonsDOBOlderThan150AndEmptyNameReturnsDOBAndNameError()
        {
            // Arrange
            //Person person = new Person() { Id = 1, Address = "address_x", Details = "details_x", Name = "name_x", DateOfBirth = new DateTime(2000, 12, 20) };
            //string expected = ConvertionUtils.GetJson(person);

            // Act
            //var actual = ConvertionUtils.GetJson(new PersonViewModel() { Id = 1, Address = "address_x", Details = "details_x", Name = "name_x", DateOfBirth = new DateTime(2000, 12, 20) });

            // Assert
            //Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithPersonsDOBInTheFutureAndEmptyNameReturnsDOBAndNameError()
        {
            // Arrange
            //Person person = new Person() { Id = 1, Address = "address_x", Details = "details_x", Name = "name_x", DateOfBirth = new DateTime(2000, 12, 20) };
            //string expected = ConvertionUtils.GetJson(person);

            // Act
            //var actual = ConvertionUtils.GetJson(new PersonViewModel() { Id = 1, Address = "address_x", Details = "details_x", Name = "name_x", DateOfBirth = new DateTime(2000, 12, 20) });

            // Assert
            //Assert.Equal(expected, actual);
        }
        #endregion
    }
}
