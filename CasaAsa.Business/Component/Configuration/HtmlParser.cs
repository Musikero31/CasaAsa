using AutoMapper.Configuration.Conventions;
using CasaAsa.Core.Configuration;
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

        //public async Task<string> ParseByReportType<T>(T model, string reportType) where T : ITemplateFields
        //{
        //    // Retrieve setting from the database
        //    var value = await _settingComponent.RetrieveApplicationSettingValueAsync(reportType);

        //    // Retrieve the full file.
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        throw new ArgumentNullException($"Report type {reportType} not found.");
        //    }

        //    // Parse the html to string.
        //    var path = Path.Combine(AppContext.BaseDirectory, value);

        //    var doc = new HtmlDocument();
        //    doc.Load(path);

        //    //var result = doc.DocumentNode.OuterHtml.Replace("{{Username}}", model.Username)
        //    //                                       .Replace("{{FullName}}", model.FullName)
        //    //                                       .Replace("{{CurrentYear}}", DateTime.Now.Year.ToString());

        //    return doc.DocumentNode.OuterHtml;
        //}

        public async Task<string> ParseByReportTypeAsync(TemplateFields template, string reportType)
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
                        
            var textNodes = doc.DocumentNode.SelectNodes("//p | //div[@class='button-wrapper']");
            
            foreach (var item in template.OtherParameters)
            {
                var textNode = textNodes.FirstOrDefault(x => x.InnerHtml.Contains("{{" + item.Key + "}}"))
                               ?? throw new ArgumentNullException($"{item.Key} does not exist");

                var newText = textNode.InnerHtml.Replace("{{" + item.Key + "}}", item.Value);

                textNode.InnerHtml = newText;
            }

            var result = doc.DocumentNode.OuterHtml.Replace("{{Username}}", template.Username)
                                                   .Replace("{{FullName}}", template.FullName)
                                                   .Replace("{{CurrentYear}}", DateTime.Now.Year.ToString());

            return result;
        }
    }
}
