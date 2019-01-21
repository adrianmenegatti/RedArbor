using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedArbor.Api.Messages;
using RedArbor.Domain.Models;
using RedArbor.Domain.Services;
using System;
using System.Threading.Tasks;

namespace RedArbor.Api.Controllers
{
	[Route("api/redarbor")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
		private readonly IEmployeeService employeeService;

		public EmployeeController(IEmployeeService employeeService)
		{
			this.employeeService = employeeService;
		}

		/// <summary>
		/// Retrieves the list of employees
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var result = await employeeService.GetAll();

				if (result == null)
				{
					return BadRequest("Item not found");
				}

				return Ok(result);

			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, exception);
			}
		}

		/// <summary>
		/// Retrieves an employee by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var result = await employeeService.GetById(id);

				if (result == null)
				{
					return BadRequest("Item not found");
				}

				return Ok(result);

			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, exception);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post(EmployeeRequest request)
		{
			var employee = Mapper.Map<Employee>(request);
			return await AddOrUpdate(employee);
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] EmployeeRequest request)
		{
			var employee = Mapper.Map<Employee>(request);
			employee.Id = id;

			return await AddOrUpdate(employee);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await employeeService.Delete(id);

				return Ok();

			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, exception);
			}
		}

		private async Task<IActionResult> AddOrUpdate(Employee employee)
		{
			try
			{
				await employeeService.AddOrUpdate(employee);

				return Ok();

			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, exception);
			}
		}
	}
}