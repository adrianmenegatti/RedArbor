using Microsoft.Extensions.Configuration;
using RedArbor.Domain.Models;
using RedArbor.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedArbor.Data
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private const string IdFieldName = "Id";
		private readonly string connectionString;

		public EmployeeRepository(IConfiguration configuration)
		{
			connectionString = ConfigurationExtensions.GetConnectionString(configuration, "redarbor");
		}

		public async Task AddOrUpdate(Employee employee)
		{
			var command =
				employee.Id == 0 ?
				GetInsertCommand(employee) :
				GetUpdateCommand(employee);

			var parameters = GetValuesArray(employee, employee.Id != 0);

			await ExecuteNonQuery<int>(command, parameters);
		}

		public async Task Delete(int id)
		{
			var command = GetDeleteCommand();
			var parameters = new object[] { id };

			await ExecuteNonQuery<int>(command, parameters);
		}

		public async Task<IEnumerable<Employee>> GetAll()
		{
			var command = GetSelectAllCommand();

			async Task<List<Employee>> callback(SqlDataReader reader)
			{

				var result = new List<Employee>();

				while (await reader.ReadAsync())
				{
					result.Add(ReaderToEntity(reader));
				}

				return result;
			}

			return await ExecuteReader(callback, command);
		}

		public async Task<Employee> GetById(int id)
		{
			var command = GetParametrizedSelectCommand();
			var parameters = new object[] { id };

			async Task<Employee> callback(SqlDataReader reader)
			{

				if (!reader.Read())
					return null;

				return await Task.FromResult(ReaderToEntity(reader));
			}

			return await ExecuteReader(callback, command, parameters );
		}

		private async Task<int> ExecuteNonQuery<T>(string sqlCommand, params object[] parameters)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();

				using (var command = CreateCommand(connection, sqlCommand, parameters))
				{
					return await command.ExecuteNonQueryAsync();
				}
			}
		}

		private async Task<T> ExecuteReader<T>(Func<SqlDataReader, Task<T>> func, string sqlCommand, params object[] param)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				await connection.OpenAsync();

				using (var command = CreateCommand(connection, sqlCommand, param))
				{
					using (var reader = await command.ExecuteReaderAsync())
					{
						return await func(reader);
					}
				}
			}
		}

		private Employee ReaderToEntity(IDataReader reader)
		{
			var result = new Employee();
			var type = result.GetType();
			var properties = type.GetProperties();

			foreach (var property in properties)
			{
				var val = reader[property.Name];
				property.SetValue(result, val);
			}

			return result;
		}

		private SqlCommand CreateCommand(SqlConnection connection, string sqlText, params object[] parameters)
		{
			var command = connection.CreateCommand();

			if (parameters != null)
			{
				for (var i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(new SqlParameter { ParameterName = $"@p{i}", Value = parameters[i] });
				}
			}

			command.CommandText = sqlText;
			command.CommandType = CommandType.Text;

			return command;
		}

		private string GetInsertCommand(Employee employee)
		{
			var type = employee.GetType();
			var fields = type.GetProperties();
			var insertCommand = "insert into employees (";
			var fieldsList = new StringBuilder();
			var valuesList = new StringBuilder();

			valuesList.Append("values (");
			fields.Where(f => f.Name != IdFieldName).Select((e, i) => new { e.Name, Index = i }).ToList().ForEach(item =>
			{

				fieldsList.Append($"{item.Name},");
				valuesList.Append($"@p{item.Index},");

			});

			fieldsList.Remove(fieldsList.Length - 1, 1).Append(")");
			valuesList.Remove(valuesList.Length - 1, 1).Append(")");

			return $"{insertCommand} {fieldsList} {valuesList}";
		}

		private string GetUpdateCommand(Employee employee)
		{
			var type = employee.GetType();
			var fields = type.GetProperties();
			var updateCommand = "update employees set";
			var fieldsList = new StringBuilder();

			fields.Where(f => f.Name != IdFieldName && !IsNullOrEmpty(f.GetValue(employee))).Select((e, i) => new { e.Name, Index = i }).ToList().ForEach(item =>
			{
				fieldsList.Append($"{item.Name}=@p{item.Index},");
				
			});

			fieldsList.Remove(fieldsList.Length - 1, 1).Append($" where id={employee.Id}");

			return $"{updateCommand} {fieldsList.ToString()}";
		}

		private string GetDeleteCommand()
		{
			return $"delete from employees where {IdFieldName}=@p0";
		}

		private string GetSelectAllCommand()
		{
			var type = typeof(Employee);
			var fieldsList = new StringBuilder();

			type.GetProperties()
				.Select(p => p.Name)
				.ToList()
				.ForEach(s =>
				{
					fieldsList.Append($"{s},");
				});

			fieldsList.Remove(fieldsList.Length - 1, 1);

			return $"select {fieldsList} from employees";
		}

		private string GetParametrizedSelectCommand()
		{
			return $"{GetSelectAllCommand()} where {IdFieldName}=@p0";
		}

		private object[] GetValuesArray(Employee employee, bool excludeNulls)
		{
			var type = employee.GetType();
			var fields = type.GetProperties().Where(f => f.Name != IdFieldName);

			var values = fields.Select(f => f.GetValue(employee));

			return excludeNulls ? values.Where(v => !IsNullOrEmpty(v)).ToArray() : values.ToArray();
		}

		private bool IsNullOrEmpty(object value)
		{
			if (value is null)
				return true;

			var type = value.GetType();
			return type.IsValueType
				&& Equals(value, Activator.CreateInstance(type));
		}
	}
}
