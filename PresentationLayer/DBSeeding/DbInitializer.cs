using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Helper;
using DomainLayer.Data;
using DomainLayer.Models;
using LeaveManagement.Models;

namespace DomainLayer.DBSeeding
{
    public class DbInitializer
    {
        /// <summary>
        /// Seeds the database with initial data if it is empty.
        /// </summary>
        /// <param name="context">The database context to seed.</param>
        public static void Seed(LeaveDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            context.Database.EnsureCreated();

            // Check if there are any users or employees already in the database
            if (context.Users.Any() && context.Employee.Any()) return;

            // List of initial users to seed
            var users = new List<User>
            {
                new User
                {
                    UserName = "Batman",
                    Password = PasswordHash.Hashing("BruceWyane@123"),
                    Email = "Batman@example.com",
                    Phone = "9812320579",
                    Address = "1007 Mountain Drive, Gotham",
                    Gender = Gender.Male,
                    JoinedDate = DateTime.Now,
                    Position = Position.HRManager,
                    DateOfBirth = new DateTime(1980, 2, 19),
                    Roles = Roles.SuperAdmin,
                    CreatedBy = "System"
                }
            };

            // List of initial employees to seed
            var employees = new List<Employee>
            {
                new Employee
                {
                    UserName = "Batman",
                    Password = PasswordHash.Hashing("BruceWyane@123"),
                    Email = "Batman@example.com",
                    Phone = "9812320579",
                    Address = "1007 Mountain Drive, Gotham",
                    Gender = Gender.Male,
                    JoinedDate = DateTime.Now,
                    DateOfBirth = new DateTime(1980, 2, 19),
                    Position = Position.HRManager,
                    Status = Status.Active,
                    Roles = Roles.SuperAdmin,
                    CreatedBy = "System"
                }
            };

            // Add the initial users to the context
            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            // Add the initial employees to the context
            foreach (var employee in employees)
            {
                context.Employee.Add(employee);
            }

            // Save the changes to the database
            context.SaveChanges();
        }
    }
}
