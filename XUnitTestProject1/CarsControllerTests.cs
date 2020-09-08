using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Test_api.Models;
using MongoDB.Bson;
using System.Linq;
using Newtonsoft.Json;

namespace Test_api.IntegrationTests
{
    public class CarsControllerTests: IntegrationTest
    {

        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnNotFound()
        {
            // Act
            var response = await TestClient.GetAsync("");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAll_WithGoodUrl_ReturnOk()
        {
            // Act
            var response = await TestClient.GetAsync("api/cars");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_WithoutID_InternalServerError()
        {
            // Act
            var response = await TestClient.GetAsync("api/cars/someID");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError); 
        }

        [Fact]
        public async Task Get_WithCorrectID_ReturnOK()
        {
            // Act
            var response = await TestClient.GetAsync("api/cars/5f5686d1ef2a90a58191f7b3");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task POST_WithCorrectData_ReturnOK()
        {
            // Arrange
            var url = "api/cars";

            var body = new Car { Name = "Car_6", Description = "This is Ferrari" };
            // Act
            var response = await TestClient.PostAsync(url, GetStringContent(body));
            var value = await response.Content.ReadAsStringAsync();
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_WithCorrectData_ReturnOK()
        {
            // Arrange
            var url = "api/cars/5f568789b049a4725201c217";

            // Act
            var response = await TestClient.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task PUT_WithCorrectData_ReturnOK()
        {
            // Arrange
            var url = "api/cars";

            var body = new CarPrototype {Id = "5f572af73f6cde55d616e758",  Description="yyyyyy" };
            // Act
            var response = await TestClient.PostAsync(url, GetStringContent(body));
            //var value = await response.Content.ReadAsStringAsync();
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
