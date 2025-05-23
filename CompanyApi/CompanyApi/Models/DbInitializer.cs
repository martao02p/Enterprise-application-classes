using System.Linq;

public static class DbInitializer
{
    public static void Initialize(CompanyDbContext context)
    {
        if (context.Departments.Any())
        {
            return; // jesli dane juz sa to return wtedy
        }

        var departments = new Department[]
        {
            new Department { Name = "Sales" },
            new Department { Name = "Marketing" },
            new Department { Name = "Research" },
            new Department { Name = "Administration" }
        };
        context.Departments.AddRange(departments);
        context.SaveChanges();

        var employees = new Employee[]
        {
            new Employee { FirstName = "Daenerys", LastName = "Targaryen", Salary = 8000, Bonus = 900, DepartmentId = 4 },
            new Employee { FirstName = "Jaime", LastName = "Lannister", ManagerId = 1, Salary = 6000, DepartmentId = 4 },
            new Employee { FirstName = "Jon", LastName = "Snow", ManagerId = 1, Salary = 6000, DepartmentId = 4 },
            new Employee { FirstName = "Jorah", LastName = "Mormont", ManagerId = 2, Salary = 3500, Bonus = 300, DepartmentId = 2 },
            new Employee { FirstName = "Davos", LastName = "Seaworth", ManagerId = 2, Salary = 3000, Bonus = 500, DepartmentId = 1 },
            new Employee { FirstName = "Arya", LastName = "Stark", ManagerId = 3, Salary = 4900, DepartmentId = 3 },
            new Employee { FirstName = "Theon", LastName = "Greyjoy", ManagerId = 3, Salary = 5300, DepartmentId = 3 }
        };
        context.Employees.AddRange(employees);
        context.SaveChanges();
    }
}
