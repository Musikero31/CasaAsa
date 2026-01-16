using CasaAsa.Data.Models;
using CasaAsa.Data.Repository;
using System.Text;

namespace CasaAsa.Business.Component
{
    public class SettingsComponent
    {
        private readonly IRepository<ApplicationSetting> _repository;

        public SettingsComponent(IRepository<ApplicationSetting> repository)
        {
            _repository = repository;
        }

        public async Task<string> RetrieveApplicationSettingAsync(string code)
        {
            var result = await _repository.FindAsync(x => x.Code == code && x.ActiveStatus);

            return result.FirstOrDefault()?.Value ?? string.Empty;
        }
    }
}
