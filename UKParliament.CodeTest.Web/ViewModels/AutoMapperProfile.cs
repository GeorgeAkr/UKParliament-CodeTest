using Microsoft.AspNetCore.Identity;
using AutoMapper;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Web.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, PersonViewModel>()
                .ReverseMap();
        }
    }
}
