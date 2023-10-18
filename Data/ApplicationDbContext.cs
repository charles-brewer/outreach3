using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using outreach3.Data.Ministries;

namespace outreach3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Church> Churches { get; set; } = default!;



        public DbSet<VisitationsMembers> VisitationMembers { get; set; } = default!;

        public DbSet<Member> Members { get; set; } = default!;

        public DbSet<Mission> Missions { get; set; } = default!;

        public DbSet<Resident> Residents { get; set; } = default!;

        public DbSet<Visitation> Visitations { get; set; } = default!;
        public DbSet<MissionMap> MissionMaps { get; set; } = default!;
        public DbSet<MapMarker> MapMarkers { get; set; } = default!;


        //For Unit Testing
        public static List<Church> GetSeededChurches()
        {
            return new List<Church>()
            {
               new Church
               {
                   Name="ChurchA", ChurchId=1, ChurchAddress="97 ABC St", ChurchFullName= "The Church A", ChurchPhone="904)904-9049", PastorsName="Pastor A" 
               },
                new Church
                {
                    Name="ChurchB", ChurchId=2, ChurchAddress="98 ABC St", ChurchFullName= "The Church B", ChurchPhone="904)904-9049", PastorsName="Pastor B"
                },
                new Church
                {
                    Name="ChurchC", ChurchId=3, ChurchAddress="99 ABC St", ChurchFullName= "The Church C", ChurchPhone="904)904-9049", PastorsName="Pastor C"
                }
                
            };
        }


        //For Unit Testing
        public static Church GetSeededChurch()
        {
            return
               new Church
               {
                   Name = "ChurchB",
                   ChurchId = 1,
                   ChurchAddress = "97 ABC St",
                   ChurchFullName = "The Church B",
                   ChurchPhone = "904)904-9049",
                   PastorsName = "Pastor B"
               };
        }
        public static List<Member>GetSeededMembers()
        {
            return new List<Member>()
            {
                new Member
                {
                    Name="Charlie", ChurchId = 1, Approved=true, Email="a@a.com", MemberId=1
                }
            };
        }


        public async virtual Task<List<Church>> GetChurchesAsync()
        {
            return await Churches
                .OrderBy(c => c.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async virtual Task<Church> GetChurchAsync(int churchId)
        {
            return await Churches.FirstOrDefaultAsync(c=>c.ChurchId==churchId);                
        }

        public async virtual Task<List<Church>> GetChurchesAsync(int churchId)
        {            
            return await Churches
                .Where(c=>c.ChurchId==churchId)
                .OrderBy(c => c.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async virtual Task<Member> GetMemberAsync(string name)
        {
            return await Members.FirstOrDefaultAsync(c => c.Name == name);
               
        }

        //public DbSet<outreach3.Data.Ministries.ChurchGoal> ChurchGoals { get; set; } = default!;

        public DbSet<outreach3.Data.Ministries.FollowUp> FollowUp { get; set; } = default!;
    }


    public static class Utilities
    {
        #region snippet1
        public static DbContextOptions<ApplicationDbContext> TestDbContextOptions()
        {
            // Create a new service provider to create a new in-memory database.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance using an in-memory database and 
            // IServiceProvider that the context should resolve all of its 
            // services from.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
        #endregion
    }

}