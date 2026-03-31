namespace CasaAsa.Business.Component.Configuration
{
    public interface ISettingsComponent
    {
        Task<string> RetrieveApplicationSettingValueAsync(string code);
    }
}