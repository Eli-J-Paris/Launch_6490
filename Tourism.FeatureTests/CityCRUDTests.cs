using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tourism.DataAccess;
using Tourism.Models;

namespace Tourism.FeatureTests
{
    public class CityCRUDTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CityCRUDTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
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
        public async void Index_IncludesLinktoNew()
        {
            var context = GetDbContext();
            var client = _factory.CreateClient();

            context.States.Add(new State { Name = "Iowa", Abbreviation = "IA" });
            context.SaveChanges();

            var response = await client.GetAsync("/states/1/cities");
            var html = await response.Content.ReadAsStringAsync();

            var expectedLink = "<a href=\"/states/1/cities/new\">New City</a>";

            Assert.Contains(expectedLink, html);
        }

        [Fact]
        public async void New_ReturnsNewForm()
        {
            var context = GetDbContext();
            var client = _factory.CreateClient();

            context.States.Add(new State { Name = "Iowa", Abbreviation = "IA" });
            context.SaveChanges();

            var response = await client.GetAsync("/states/1/cities/new");
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("Add city to Iowa", html);
            Assert.Contains("<form method=\"post\" action=\"/states/1/cities\">", html);
        }

        [Fact]
        public async void Create_AddsCityToDatabase()
        {
            var context = GetDbContext();
            var client = _factory.CreateClient();

            context.States.Add(new State { Name = "Iowa", Abbreviation = "IA" });
            context.SaveChanges();

            var formData = new Dictionary<string, string>
            {
                { "Name", "Des Moines" }
            };

            var response = await client.PostAsync("/states/1/cities", new FormUrlEncodedContent(formData));
            var html = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("Cities in Iowa", html);
            Assert.Contains("Des Moines", html);

            Assert.Equal(1, context.Cities.Count());
            Assert.Equal("Des Moines", context.Cities.First().Name);
        }
    }
}
