using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using UKParliament.CodeTest.IntegrationTests;
using UKParliament.CodeTest.Web.ViewModels;
using UKParliament.CodeTest.IntegrationTests.Utilities;

namespace UKParliament.CodeTest.IntegrationTests.Controllers
{
    public class PersonControllerTests : IClassFixture<CustomWebApplicationFactory<Web.Program>>
    {
        private readonly HttpClient Client;
        private readonly string Path = "/api/";

        public PersonControllerTests(CustomWebApplicationFactory<Web.Program> factory)
        {
            Client = factory.CreateClient();
        }

        [Fact]
        public async Task WithNoIdCallGetRetunsAllPersons()
        {
            // Arrange
            var expected = true;

            // Act
            var httpResponse = await Client.GetAsync(Path + "person");
            httpResponse.EnsureSuccessStatusCode();
            var content = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<PersonViewModel>>(content).Count > 1;

            // Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task WithIdTwoCallGetRetunsPersonWithIdTwo()
        {
            // Arrange
            var id = 2;
            var expected = id;

            // Act
            var httpResponse = await Client.GetAsync(Path + $"person/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadAsStringAsync();
            var actual = ConvertionUtils.GetPersonViewModel(response).Id;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithNonExistentIdCallGetRetunsNotFound()
        {
            // Arrange
            var id = 6;
            var expected = HttpStatusCode.NotFound;

            // Act
            var httpResponse = await Client.GetAsync(Path + $"persons/{id}");
            var actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithValidPersonCallPutRetunsOK()
        {
            // Arrange
            var id = 1;
            PersonViewModel personModel = new PersonViewModel() { Id = id, Name = "Name_e", Address = "Address_e", Details = "details_e", DateOfBirth = new DateTime(2000, 12, 28) };
            HttpContent content = new StringContent(ConvertionUtils.GetJson(personModel), Encoding.UTF8, "application/json");
            var expected = HttpStatusCode.NoContent;

            // Act
            var httpResponse = await Client.PutAsync(Path + $"person/person", content);
            var actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithNullPersonCallPutRetunsBadRequest()
        {
            // Arrange
            PersonViewModel personModel = null;
            var person = ConvertionUtils.GetJson(personModel);
            HttpContent content = new StringContent(person, Encoding.UTF8, "application/json");
            var expected = HttpStatusCode.BadRequest;

            // Act
            var httpResponse = await Client.PutAsync(Path + $"person/person", content);
            var actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithNonExistentPersonCallPutRetunsNotFound()
        {
            // Arrange
            var id = 55;
            PersonViewModel personModel = new PersonViewModel() { Name = "Name_e", Address = "Address_e", Details = "details_e", DateOfBirth = new DateTime(2000, 12, 28) };
            var person = ConvertionUtils.GetJson(personModel);
            HttpContent content = new StringContent(person, Encoding.UTF8, "application/json");
            var expected = HttpStatusCode.NotFound;

            // Act
            var httpResponse = await Client.PutAsync(Path + $"person/person", content);
            var actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithValidPersonCallPostRetunsNewPerson()
        {
            // Arrange
            var id = 5;
            PersonViewModel person = new PersonViewModel() { Name = "Name_f", Address = "Address_f", Details = "details_f", DateOfBirth = new DateTime(2000, 12, 29) };
            var personJson = ConvertionUtils.GetJson(person);
            HttpContent content = new StringContent(personJson, Encoding.UTF8, "application/json");
            person.Id = id;
            var expected = ConvertionUtils.GetJson(person);

            // Act
            var httpResponse = await Client.PostAsync(Path + $"person/person", content);
            httpResponse.EnsureSuccessStatusCode();
            var actual = await httpResponse.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithNullPersonCallPostRetunsBadRequest()
        {
            // Arrange
            PersonViewModel personModel = null;
            var person = ConvertionUtils.GetJson(personModel);
            HttpContent content = new StringContent(person, Encoding.UTF8, "application/json");
            var expected = HttpStatusCode.BadRequest;

            // Act
            var httpResponse = await Client.PostAsync(Path + $"person/person", content);
            var actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithNoIdPersonCallDeleteRetunsNotFound()
        {
            // Arrange
            var expected = HttpStatusCode.NotFound;

            // Act
            var httpResponse = await Client.DeleteAsync(Path + $"person/");
            var actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithNonExistentIdPersonCallDeleteRetunsNotFound()
        {
            // Arrange
            var id = 55;
            var expected = HttpStatusCode.NotFound;

            // Act
            var httpResponse = await Client.DeleteAsync(Path + $"persons/{id}");
            var actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task WithValidIdPersonCallDeleteReturnsOK()
        {
            // Arrange
            var id = 4;
            var expected = HttpStatusCode.OK;

            // Act
            var httpResponse = await Client.DeleteAsync(Path + $"person/{id}");
            var actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AfterDeletionCallGetWithDeletedIdReturnsNotFound()
        {
            // Arrange
            var id = 3;
            var expected = HttpStatusCode.OK;

            // Act
            var httpResponse = await Client.DeleteAsync(Path + $"person/{id}");
            var actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);

            // Arrange
            expected = HttpStatusCode.NotFound;

            // Act
            httpResponse = await Client.GetAsync(Path + $"persons/{id}");
            actual = httpResponse.StatusCode;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

