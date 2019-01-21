using RedArbor.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedArbor.Domain.Services
{
	public interface IEmployeeService
	{
		Task<IEnumerable<Employee>> GetAll();
		Task<Employee> GetById(int id);
		Task AddOrUpdate(Employee employee);
		Task Delete(int id);
	}
}
