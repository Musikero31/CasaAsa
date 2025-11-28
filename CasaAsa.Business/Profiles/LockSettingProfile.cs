using AutoMapper;
using DataModel = CasaAsa.Data.Models;
using CoreModel = CasaAsa.Core.BusinessModels;

namespace CasaAsa.Business.Profiles
{
    public class LockSettingProfile : Profile
    {
        public LockSettingProfile()
        {
            CreateMap<DataModel.LockOrder, CoreModel.LockOrder>();
        }
    }
}
