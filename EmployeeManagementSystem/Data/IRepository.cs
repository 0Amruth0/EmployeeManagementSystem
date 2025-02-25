using System.Collections.Generic;
using System.Data;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Data
{
    public interface IRepository<T>
    {
        void Add(Employee e);
        void Update(Employee e, int action);
        void Delete(int id);
        List<T> GetAll();
        DataTable GetById(int id);
    }
}
