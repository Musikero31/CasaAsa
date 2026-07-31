using AutoMapper;
using CasaAsa.Business.Component.Configuration;
using CoreModel = CasaAsa.Core.BusinessModels;
using DataModel = CasaAsa.Data.Entities;

namespace CasaAsa.Business.Profiles
{
    public class DocumentsMapProfiles : Profile
    {
        public DocumentsMapProfiles()
        {
            CreateMap<CoreModel.Documents, DataModel.Documents>()
                .ForMember(dest => dest.Id, source => source.MapFrom(src => src.DocumentId))
                .IgnoreAuditFields()
                .ReverseMap();
        }
    }
}
