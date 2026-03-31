using CasaAsa.Core.Configuration;

namespace CasaAsa.Business.Component.Configuration
{
    public interface IMailComponent
    {
        void SendMail(Mail mail);
    }
}