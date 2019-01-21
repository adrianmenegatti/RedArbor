using RedArbor.Domain.Models;
using RedArbor.Domain.Repositories;
using RedArbor.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedArbor.Services
{
	public class EmployeeService : IEmployeeService
	{
		IEmployeeRepository employeeRepository;

		public EmployeeService(IEmployeeRepository employeeRepository)
		{
			this.employeeRepository = employeeRepository;
		}

		public async Task AddOrUpdate(Employee employee)
		{
			await employeeRepository.AddOrUpdate(employee);
		}

		public async Task Delete(int id)
		{
			await employeeRepository.Delete(id);
		}

		public async Task<IEnumerable<Employee>> GetAll()
		{
			return await employeeRepository.GetAll();
		}

		public async Task<Employee> GetById(int id)
		{
			return await employeeRepository.GetById(id);
		}
	}
}
