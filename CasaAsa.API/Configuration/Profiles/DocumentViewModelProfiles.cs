using AutoMapper;
using CasaAsa.API.Models;
using CasaAsa.Core.BusinessModels;

namespace CasaAsa.API.Configuration.Profiles
{
    public class DocumentViewModelProfiles : Profile
    {
        public DocumentViewModelProfiles()
        {
            CreateMap<DocumentViewModel, Documents>()
                .ReverseMap();
            CreateMap<DocumentUploadRequest, Documents>()
                .ReverseMap();
        }
    }
}
