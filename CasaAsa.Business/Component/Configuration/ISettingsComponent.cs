namespace CasaAsa.Business.Component.Configuration
{
    public interface ISettingsComponent
    {
        Task<string> RetrieveApplicationSettingAsync(string code);
    }
}