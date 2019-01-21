using AutoMapper;
using RedArbor.Api.Messages;
using RedArbor.Domain.Models;

namespace RedArbor.Api.MapperProfiles
{
	public class EmployeeProfile : Profile
	{
		public EmployeeProfile()
		{
			CreateMap<EmployeeRequest, Employee>();
		}
	}
}
