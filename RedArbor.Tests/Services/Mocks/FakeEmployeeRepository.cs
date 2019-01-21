using RedArbor.Domain.Models;
using RedArbor.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Tests.Services.Mocks
{
	public class FakeEmployeeRepository : IEmployeeRepository
	{
		private IList<Employee> employees;

		public FakeEmployeeRepository()
		{
			Initialize();
		}

		public async Task AddOrUpdate(Employee employee)
		{
			if (employee.Id == 0)
			{
				await Task.Run(() =>  employees.Add(employee));
			}
			else
			{
				var current = await Task.Run(() => employees.FirstOrDefault(x => x.Id == employee.Id));
				if (current != null)
				{
					var index = employees.IndexOf(current);
					employees[index] = employee;
				}
			}
		}

		public async Task Delete(int id)
		{
			if (employees.Any(x => x.Id == id))
				await Task.Run(() => employees.Remove(employees.First(x => x.Id == id)));
		}

		public async Task<IEnumerable<Employee>> GetAll()
		{
			return await Task.FromResult(employees);
		}

		public async Task<Employee> GetById(int id)
		{
			var employee = employees.FirstOrDefault(x => x.Id == id);

			return await Task.FromResult(employee);
		}

		private void Initialize()
		{
			employees = new List<Employee>
			{
				new Employee
				{
					Id = 1,
					CompanyId= 1,
					CreatedOn= DateTime.Parse("2000-01-01 00:00:00"),
					DeletedOn= DateTime.Parse("2000-01-01 00:00:00"),
					Email= "test1@test.test.tmp",
					Fax= "000.000.000",
					Name= "test1",
					LastLogin= DateTime.Parse("2000-01-01 00:00:00"),
					Password= "test",
					PortalId= 1,
					RoleId= 1,
					StatusId= 1,
					Telephone= "000.000.000",
					UpdatedOn= DateTime.Parse("2000-01-01 00:00:00"),
					UserName= "test1"
				}
			};
		}
	}
}



