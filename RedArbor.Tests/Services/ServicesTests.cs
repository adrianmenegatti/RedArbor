using RedArbor.Domain.Models;
using RedArbor.Domain.Services;
using RedArbor.Services;
using RedArbor.Tests.Services.Mocks;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RedArbor.Tests.Services
{
	public class ServicesTests
	{
		IEmployeeService service;

		public ServicesTests()
		{
			service = new EmployeeService(new FakeEmployeeRepository());
		}

		[Fact]
		public async Task GetAllReturnsListOfEmployees()
		{
			var actual = await service.GetAll();

			Assert.Single(actual);
		}

		[Fact]
		public async Task GetByIdReturnsEmployeeForCorrectId()
		{
			var correctId = 1;
			var actual = await service.GetById(correctId);

			Assert.NotNull(actual);
			Assert.Equal(1, actual.Id);
		}

		[Fact]
		public async Task GetByIdReturnsNullForIncorrectId()
		{
			var incorrectId = 2;
			var actual = await service.GetById(incorrectId);

			Assert.Null(actual);
		}

		[Fact]
		public async Task AddOrUpdateAddsNewRecord()
		{
			var newEmployee = new Employee
			{
				Name = "test",
				UserName = "test"
			};

			await service.AddOrUpdate(newEmployee);
			var actual = await service.GetAll();

			Assert.Equal(2, actual.Count());
		}

		[Fact]
		public async Task AddOrUpdateUpdatesRecord()
		{
			var employee = new Employee
			{
				Id = 1,
				UserName = "testUpdated"
			};

			await service.AddOrUpdate(employee);
			var actual = await service.GetById(employee.Id);

			Assert.Equal("testUpdated", actual.UserName);
		}

		[Fact]
		public async Task DeleteDeletesRecordWithValidId()
		{
			var correctId = 1;
			await service.Delete(correctId);
			var actual = await service.GetAll();

			Assert.Empty(actual);
		}

		[Fact]
		public async Task DeleteDoesNothingWithIncorrectId()
		{
			var incorrectId = 2;
			await service.Delete(incorrectId);
			var actual = await service.GetAll();

			Assert.Single(actual);
		}
	}
}
