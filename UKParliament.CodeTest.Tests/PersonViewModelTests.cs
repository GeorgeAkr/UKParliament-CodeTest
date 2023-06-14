using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;
using Xunit;

namespace UKParliament.CodeTest.Tests
{
    public class PersonViewModelTests 
    { 
        public PersonViewModelTests() { }

        [Fact]
        public void WithPersonProducesValidModel()
        {
            // Arrange
            Person person = new Person() { Id = 1, Address = "address_x", Details = "details_x", Name = "name_x", DateOfBirth = new DateTime(2000,12,20) };
            string expected = ConvertionUtils.GetJson(person);

            // Act
            var actual = ConvertionUtils.GetJson(new PersonViewModel() { Id = 1, Address = "address_x", Details = "details_x", Name = "name_x", DateOfBirth = new DateTime(2000, 12, 20) });

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}