
namespace CasaAsa.Business.Component.Configuration
{
    public interface IHtmlParser
    {
        Task<string> ParseByReportType(string reportType);
    }
}