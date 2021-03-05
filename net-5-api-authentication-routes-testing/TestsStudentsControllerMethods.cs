using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using net_5_api_with_authentication;
using net_5_api_with_authentication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.tests
{
    public class TestStudentsControllerMethods : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;


        public TestStudentsControllerMethods(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_All_Students_Should_Return_200()
        {
            // Arrange
            var client = new PrivateEndpointTestArranger(_factory).Arrange();

            // Act
            var response = await client.GetAsync("/students");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_All_Students_should_Return_At_Least_One_Student()
        {
            // Arrange
            var client = new PrivateEndpointTestArranger(_factory).Arrange();
            // Act
            var response = await client.GetAsync("/students");
            using (Stream s = await response.Content.ReadAsStreamAsync())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        IEnumerable<Student> studentList = serializer.Deserialize<IEnumerable<Student>>(reader);
                        // Assert
                        Assert.True(studentList.Any());
                    }
                }
            }
        }

        [Fact]
        public async Task Student_Get_By_Id_Should_Return_200()
        {
            // Arrange
            var client = new PrivateEndpointTestArranger(_factory).Arrange();
            // Act
            var response = await client.GetAsync("/students/1");
            // Assert 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Student_Get_By_Id_Should_Return_A_Student()
        {
            // Arrange
            var client = new PrivateEndpointTestArranger(_factory).Arrange();
            // Act
            var response = await client.GetAsync("/students/1");
            var responseString = await response.Content.ReadAsStringAsync();
            Student student = JsonConvert.DeserializeObject<Student>(responseString);

            // Assert
            Assert.True(!student.ID.Equals(null));
        }

        [Fact]
        public async Task Student_Get_By_Id_Should_Return_404_Case_Student_Doesnt_Exist()
        {
            // Arrange
            var client = new PrivateEndpointTestArranger(_factory).Arrange();
            // Act
            var response = await client.GetAsync("/students/100");
            // Assert 
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Student_Post_Should_Return_Student_Created()
        {
            // Arrange
            var client = new PrivateEndpointTestArranger(_factory).Arrange();
            // Act
            var student = new Student { FirstMidName = "Marcelo", LastName = "Guimarães", EnrollmentDate = DateTime.Parse("2005-09-01") };
            string json = JsonConvert.SerializeObject(student);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/students", httpContent);
            var responseString = await response.Content.ReadAsStringAsync();
            Student storedStudent = JsonConvert.DeserializeObject<Student>(responseString);
            // Assert
            Assert.True(HttpStatusCode.Created.Equals(response.StatusCode) && storedStudent.ID > 0);
        }

        [Fact]
        public async Task Student_Put_Should_Return_204()
        {
            // Arrange
            var client = new PrivateEndpointTestArranger(_factory).Arrange();
    
            var s = new Student { ID = 2, FirstMidName = "Student Was Updated", LastName = "Student Was Updated", EnrollmentDate = DateTime.Parse("2005-09-01") };
            string json = JsonConvert.SerializeObject(s);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var putResponse = await client.PutAsync("/students/2", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
        }
        [Fact]
        public async Task Student_Delete_Should_Create_Student_Delete_And_Return_204()
        {
            // Arrange
            var client = new PrivateEndpointTestArranger(_factory).Arrange();
            // Act
            
            var student = new Student { FirstMidName = "StudentToBeDeleted", LastName = "WillBEDeleted", EnrollmentDate = DateTime.Parse("2005-09-01") };
            string json = JsonConvert.SerializeObject(student);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/students", httpContent);
            var responseString = await response.Content.ReadAsStringAsync();
            Student storedStudent = JsonConvert.DeserializeObject<Student>(responseString);

            var deleteResponse = await client.DeleteAsync($"/students/{storedStudent.ID}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}
