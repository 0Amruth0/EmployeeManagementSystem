using System;
using Serilog;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Collections.Generic;

namespace EmployeeManagementSystem
{
    class Program
    {
        static void Main()
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()  // Log to console
                .WriteTo.File("logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                IRepository<Employee> employeeRepo = new Repository<Employee>();
                EmployeeService service = new EmployeeService(employeeRepo, Log.Logger);

                Console.WriteLine("Welcome to Employee Management System !!");
                Console.WriteLine("Press the corresponding number to access the functionality");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Get All Employees");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Get Employee By Id");

                // Get user input for action
                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1:
                        // Add Employee
                        Employee employee = AddEmp();
                        service.AddEmployee(employee);
                        break;

                    case 2:
                        // Get All Employees
                        var employees = service.GetAllEmployees();
                        foreach (var e in employees)
                        {
                            Console.WriteLine($"{e.Id}: {e.Name}, {e.Age}, {e.Department}, {e.Salary}");
                        }
                        break;

                    case 3:
                        object[] arr = UpdateEmp();
                        Employee emp = (Employee)arr[0];
                        int act = Convert.ToInt32(arr[1]);
                        service.UpdateEmployee(emp,act);
                        break;

                    case 4:
                        // Delete Employee
                        int id = DeleteEmployee();
                        service.DeleteEmployee(id);
                        break;

                    case 5:
                        // Get Employee By Id
                        id = GetEmployeeById();
                        service.GetById(id);
                        break;

                    default:
                        Console.WriteLine("Invalid Number entered... exiting");
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An error occurred while running the application.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        // Method to add a new employee
        public static Employee AddEmp()
        {
            Employee employee = new Employee();

            Console.WriteLine($"Enter Employee Name :");
            employee.Name = Console.ReadLine();

            Console.WriteLine($"Enter Employee Age :");
            employee.Age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Enter Employee Department :");
            employee.Department = Console.ReadLine();

            Console.WriteLine($"Enter Employee Salary :");
            employee.Salary = Convert.ToDecimal(Console.ReadLine());

            return employee;
        }

        // Method to update an employee
        public static Object[] UpdateEmp()
        {
            Employee employee = new Employee();

            Console.WriteLine("Enter employee id : ");
            int id = Convert.ToInt32(Console.ReadLine());
            employee.Id = id;

            Console.WriteLine("Press the corresponding number to access the functionality or 0 to exit");
            Console.WriteLine("1. Update Name");
            Console.WriteLine("2. Update Department");
            Console.WriteLine("3. Update Salary");
            Console.WriteLine("4. Update Age");
            int action = Convert.ToInt32(Console.ReadLine());

            switch (action)
            {
                case 1:
                    Console.WriteLine($"Enter Employee Name :");
                    employee.Name = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine($"Enter Employee Department :");
                    employee.Department = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine($"Enter Employee Salary :");
                    employee.Salary = Convert.ToDecimal(Console.ReadLine());
                    break;
                case 4:
                    Console.WriteLine($"Enter Employee Age :");
                    employee.Age = Convert.ToInt32(Console.ReadLine());
                    break;
                default:
                    break;
            }
            object[] res = { employee, action };
            return res;
        }

        // Method to delete an employee
        public static int DeleteEmployee()
        {
            Console.WriteLine("Enter Employee ID to delete:");
            int id = Convert.ToInt32(Console.ReadLine());

            return id;
        }

        // Method to get employee by ID
        public static int GetEmployeeById()
        {
            Console.WriteLine("Enter Employee ID:");
            int id = Convert.ToInt32(Console.ReadLine());

            return id;
        }
    }
}
//using System;
//using System.Data.SqlClient;

//class EmployeeManagementSystem1
//{
//    static void Main()
//    {
//        var connectionString = "Server=EPINHYDW133C\\SQLEXPRESS;Database=ProductDB;Trusted_Connection=True;";
//        using (var connection = new SqlConnection(connectionString))
//        {
//            try
//            {
//                connection.Open();
//                Console.WriteLine("Connection Successful");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error: {ex.Message}");
//            }
//        }
//    }
//}
