using BusinessLayer.Helper;
using DomainLayer.Data;
using DomainLayer.Models;
using LeaveManagement.Models;

namespace DomainLayer.DBSeeding
{
    public class DbInitializer
       
    {
        public static void Seed(LeaveDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(LeaveManagement));
            context.Database.EnsureCreated();
            if (context.Users.Any() && context.Employee.Any()) return;

            var users = new List<User>()
            {
                new User()
                {
                    UserName = "Batman",
                Password = PasswordHash.Hashing("BruceWyane@123"),
                Email = "Batman@example.com",
                Phone = "9812320579",
                Address = "1007 Mountain Drive, Gotham",
                Gender = Gender.Male,
                JoinedDate = DateTime.Now,
                Position=Position.HRManager,
                DateOfBirth = new DateTime(1980, 2, 19),
                Roles = Roles.SuperAdmin,
                CreatedBy = "System"
                }
            };

            var employees = new List<Employee>()
            {
                new Employee()
                {
                UserName = "Batman",
                Password = PasswordHash.Hashing("BruceWyane@123" ),
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
            foreach (var user in users)
                context.Users.Add(user);
            foreach (var employe in employees)
                context.Employee.Add(employe);
            context.SaveChanges();

        }
    }
}
