using LeadManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeadManagementSystem.Data
{
    public static class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // 1. Seed Statuses if empty
                if (!context.statuses.Any())
                {
                    var statuses = new List<Status>
                    {
                        new Status { Name = "New" },
                        new Status { Name = "InProgress" },
                        new Status { Name = "Converted" },
                        new Status { Name = "NonConverted" }
                    };
                    context.statuses.AddRange(statuses);
                    context.SaveChanges();
                }

                // 2. Seed Sources if empty
                if (!context.sources.Any())
                {
                    var sources = new List<Source>
                    {
                        new Source { Name = "Website" },
                        new Source { Name = "Referral" },
                        new Source { Name = "Walk-in" },
                        new Source { Name = "Social Media" }
                    };
                    context.sources.AddRange(sources);
                    context.SaveChanges();
                }

                // 3. Seed LeadTypes if empty
                if (!context.lead_types.Any())
                {
                    var types = new List<LeadType>
                    {
                        new LeadType { Type = "Hot" },
                        new LeadType { Type = "Cold" },
                        new LeadType { Type = "Warm" }
                    };
                    context.lead_types.AddRange(types);
                    context.SaveChanges();
                }

                // 4. Seed LeadLists if empty
                if (!context.lead_lists.Any())
                {
                    var lists = new List<LeadList>
                    {
                        new LeadList { Name = "General List" },
                        new LeadList { Name = "Event Leads" }
                    };
                    context.lead_lists.AddRange(lists);
                    context.SaveChanges();
                }

                // 5. Seed Schools if empty
                if (!context.schools.Any())
                {
                    var schools = new List<School>
                    {
                        new School { Name = "Green Valley High" },
                        new School { Name = "City International School" },
                        new School { Name = "Springfield Academy" }
                    };
                    context.schools.AddRange(schools);
                    context.SaveChanges();
                }

                // 6. Seed Branches if empty
                if (!context.branches.Any())
                {
                    var schools = context.schools.ToList();
                    if (schools.Any())
                    {
                        var branches = new List<Branch>
                        {
                            new Branch { BranchName = "Main Campus", SchoolId = schools[0].Id },
                            new Branch { BranchName = "East Wing", SchoolId = schools[0].Id },
                            new Branch { BranchName = "Downtown", SchoolId = schools[1].Id },
                            new Branch { BranchName = "North Extension", SchoolId = schools[1].Id },
                            new Branch { BranchName = "Science Block", SchoolId = schools[2].Id },
                        };
                        context.branches.AddRange(branches);
                        context.SaveChanges();
                    }
                }

                // 7. Ensure at least one SalesPerson exists
                if (!context.SalesPersons.Any())
                {
                    context.SalesPersons.Add(new SalesPerson
                    {
                        Name = "John Doe",
                        Code = "SP001",
                        PhoneNumber = "1234567890",
                        Email = "john.doe@example.com",
                        PaymentType = 1,
                        FirstPayment = 1000,
                        RecurringPercentage = 5.0m
                    });
                    context.SaveChanges();
                }

                // 8. Ensure at least one User (Owner) exists
                if (!context.Users.Any())
                {
                    // Assuming RoleId 1 exists or is not strictly enforced by FK constraint if nullable (it is int so it is enforced).
                    // We need a role first? Let's check Roles table.
                    if (!context.Roles.Any())
                    {
                        context.Roles.Add(new RolesModel { Name = "Admin" });
                        context.SaveChanges();
                    }
                    
                    var role = context.Roles.First();
                    context.Users.Add(new UserModel
                    {
                        FirstName = "Admin",
                        LastName = "User",
                        Email = "admin@example.com",
                        Phone = "9876543210",
                        LoginId = "admin",
                        Password = "password", // In a real app, hash this
                        RoleId = role.Id
                    });
                    context.SaveChanges();
                }

                // 9. Seed Leads Logic (Only if no leads exist for current year)
                var currentYear = DateTime.UtcNow.Year;
                if (!context.leads.Any(l => l.DateTime.Year == currentYear))
                {
                    // Fetch required dependency IDs
                    var statusIds = context.statuses.ToList();
                    var sourceIds = context.sources.ToList();
                    var branchIds = context.branches.ToList(); 
                    var typeIds = context.lead_types.ToList();
                    var listIds = context.lead_lists.ToList();
                    var salesPersonId = context.SalesPersons.First().Id;
                    var ownerId = context.Users.First().Id;

                    if (statusIds.Any() && sourceIds.Any() && branchIds.Any())
                    {
                        var rand = new Random();
                        var leads = new List<LeadEntity>();

                        // Generate 60 random leads
                        for (int i = 0; i < 60; i++)
                        {
                            var status = statusIds[rand.Next(statusIds.Count)];
                            var branch = branchIds[rand.Next(branchIds.Count)];
                            var source = sourceIds[rand.Next(sourceIds.Count)];
                            var type = typeIds.Any() ? typeIds[rand.Next(typeIds.Count)] : null;
                            var list = listIds.Any() ? listIds[rand.Next(listIds.Count)] : null;
                            
                            // Random date in current year
                            var month = rand.Next(1, 13);
                            var day = rand.Next(1, 28); // Safe for all months
                            var date = new DateTime(currentYear, month, day);

                            leads.Add(new LeadEntity
                            {
                                Name = $"Lead {i + 1}",
                                ContactNumber = $"90000{i:D5}",
                                LeadSourceId = source.Id,
                                BranchId = branch.Id,
                                SchoolId = branch.SchoolId, // Helper to set SchoolId from Branch
                                LeadTypeId = type?.Id ?? 1,
                                LeadListId = list?.Id ?? 1,
                                StatusId = status.Id,
                                Converted = status.Name == "Converted",
                                DateTime = date, // Important: using DateTime.UtcNow kind of logic if needed, but local is fine for seeding
                                SalesPersonId = salesPersonId,
                                OwnerId = ownerId
                            });
                        }

                        context.leads.AddRange(leads);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
