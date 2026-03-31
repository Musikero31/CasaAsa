using AutoMapper;
using CasaAsa.Data.Models;

namespace CasaAsa.Business.Component.Configuration
{
    public static class AutoMapperAuditExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAuditFields<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mapping)
            where TDestination : IAuditEntity
        {
            return mapping
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());
        }
    }
}
