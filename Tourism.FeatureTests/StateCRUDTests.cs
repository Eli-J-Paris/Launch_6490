using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Tourism.DataAccess;
using Tourism.Models;

namespace Tourism.FeatureTests
{
    public class StateCRUDTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public StateCRUDTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Index_ReturnsAllStates()
        {
            var context = GetDbContext();
            var client = _factory.CreateClient();

            context.States.Add(new State { Name = "Iowa", Abbreviation = "IA" });
            context.States.Add(new State { Name = "Colorado", Abbreviation = "CO" });
            context.SaveChanges();

            var response = await client.GetAsync("/states");
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("IA", html);
            Assert.Contains("Iowa", html);
            Assert.Contains("CO", html);
            Assert.Contains("Colorado", html);
            Assert.DoesNotContain("California", html);
        }

        [Fact]
        public async void Show_ReturnsSingleState()
        {
            var context = GetDbContext();
            var client = _factory.CreateClient();

            context.States.Add(new State { Name = "Iowa", Abbreviation = "IA" });
            context.States.Add(new State { Name = "Colorado", Abbreviation = "CO" });
            context.SaveChanges();

            var response = await client.GetAsync("/states/1");
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("Iowa", html);
            Assert.DoesNotContain("Colorado", html);
        }

        private TourismContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TourismContext>();
            optionsBuilder.UseInMemoryDatabase("TestDatabase");

            var context = new TourismContext(optionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public async Task New_ReturnsForm()
        {
            //Arrange
            var client = _factory.CreateClient();
            var context = GetDbContext();
            State Colorado = new State { Name = "Coolorado", Abbreviation = "CO" };
            context.States.Add(Colorado);
            context.SaveChanges();

            //Act
            var response = await client.GetAsync($"/states/new");
            var html = await response.Content.ReadAsStringAsync();

            //Assert
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.EnsureSuccessStatusCode();
            Assert.Contains("label", html);
            Assert.Contains("input", html);
            Assert.Contains($"<form method=\"post\"", html);

        }
            //I'd test for the redirect if i had time and if it would add to db

    }
}
