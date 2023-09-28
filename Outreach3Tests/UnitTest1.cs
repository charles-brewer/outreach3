using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using outreach3.Data;
using outreach3.Pages;
using outreach3.Pages.Churches;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Outreach3Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void ChurchIndexTestForAdmin()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {

                //Arrange
                //Mock the Database, set to be in memory
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("InMemoryDb");
                var mockAppDbContext = new Mock<ApplicationDbContext>(optionsBuilder.Options);
                
                //Get Fixed Values
                var expectedChurches = ApplicationDbContext.GetSeededChurches();
                var expectedMembers = ApplicationDbContext.GetSeededMembers();
                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]{
    new Claim(ClaimTypes.Name, "Charlie"),
    new Claim(ClaimTypes.NameIdentifier, "1"),
    new Claim(ClaimTypes.Role, "Admin"),
    new Claim("custom-claim", "example claim value"),}, "mock"));

                mockAppDbContext.Setup(db => db.GetChurchesAsync()).Returns(Task.FromResult(expectedChurches));               

                var pageModel = new outreach3.Pages.Sites.IndexModel(mockAppDbContext.Object)
                {
                    PageContext = new PageContext()
                    {
                        HttpContext = new DefaultHttpContext()
                        {
                            User = user
                        }
                    }
                };
                // Act
                await pageModel.OnGetAsync();
                //Test
                var actualMessages = Assert.IsAssignableFrom<List<Church>>(pageModel.Churches);
                Assert.Equal(
                    expectedChurches.OrderBy(m => m.ChurchId).Select(m => m.Name),
                    actualMessages.OrderBy(m => m.ChurchId).Select(m => m.Name));

            }

        }




        [Fact]
        public async void ChurchIndexTestForUser()
        {
            using (var db = new ApplicationDbContext(Utilities.TestDbContextOptions()))
            {

                //Arrange
                //Mock the Database, set to be in memory
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("InMemoryDb");
                var mockAppDbContext = new Mock<ApplicationDbContext>(optionsBuilder.Options);

                //Get Fixed Values
                var expectedChurch = ApplicationDbContext.GetSeededChurch();
                var expectedMembers = ApplicationDbContext.GetSeededMembers();
                var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]{
    new Claim(ClaimTypes.Name, "Charlie"),
    new Claim(ClaimTypes.NameIdentifier, "1"),
    new Claim(ClaimTypes.Role, "User"),
    new Claim("custom-claim", "example claim value"),}, "mock"));

                mockAppDbContext.Setup(db => db.GetChurchAsync(1)).Returns(Task.FromResult(expectedChurch));
                mockAppDbContext.Setup(db => db.GetMemberAsync("Charlie")).Returns(Task.FromResult(expectedMembers.FirstOrDefault()));

                var pageModel = new outreach3.Pages.Sites.IndexModel(mockAppDbContext.Object)
                {
                    PageContext = new PageContext()
                    {
                        HttpContext = new DefaultHttpContext()
                        {
                            User = user
                        }
                    }
                };
                // Act
                await pageModel.OnGetAsync();
                //Test
                var actualChurch = pageModel.Churches.FirstOrDefault();
               

                Assert.Equal("ChurchB", actualChurch.Name);

            }

        }

    }




}