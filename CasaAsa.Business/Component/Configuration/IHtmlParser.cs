using CasaAsa.Core.Configuration;

namespace CasaAsa.Business.Component.Configuration
{
    public interface IHtmlParser
    {
        Task<string> ParseByReportTypeAsync(TemplateFields template, string reportType);
    }
}