using Serilog;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using System.Collections.Generic;
using System.Data;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _repository;
        private readonly ILogger _logger;

        public EmployeeService(IRepository<Employee> repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void AddEmployee(Employee emp)
        {
            _repository.Add(emp);
            _logger.Information($"Log: Employee {emp.Name} added successfully!");
        }

        public void UpdateEmployee(Employee emp)
        {
            _repository.Update(emp);
            _logger.Information($"Log: Employee {emp.Name} updated successfully!");
        }

        public void DeleteEmployee(int id)
        {
            _repository.Delete(id);
            _logger.Information($"Log: Employee with ID {id} deleted successfully!");
        }

        public List<Employee> GetAllEmployees()
        {
            return _repository.GetAll();
            _logger.Information($"Log: All Employees data was fetched");
        }

        public void GetById(int id) { 
            _repository.GetById(id);
            _logger.Information($"Log: Details of Employee with id {id} were fetched.");
        }
    }
}
