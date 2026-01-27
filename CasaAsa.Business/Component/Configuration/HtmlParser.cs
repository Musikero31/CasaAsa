using HtmlAgilityPack;

namespace CasaAsa.Business.Component.Configuration
{
    public class HtmlParser : IHtmlParser
    {
        private readonly ISettingsComponent _settingComponent;

        public HtmlParser(ISettingsComponent settingComponent)
        {
            _settingComponent = settingComponent;
        }

        public async Task<string> ParseByReportType(string reportType)
        {
            // Retrieve setting from the database
            var value = await _settingComponent.RetrieveApplicationSettingValueAsync(reportType);

            // Retrieve the full file.
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException($"Report type {reportType} not found.");
            }

            // Parse the html to string.
            var path = Path.Combine(AppContext.BaseDirectory, value);

            var doc = new HtmlDocument();
            doc.Load(path);

            return doc.DocumentNode.OuterHtml;
        }
    }
}
