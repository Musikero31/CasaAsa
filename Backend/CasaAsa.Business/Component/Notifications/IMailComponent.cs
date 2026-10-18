using CasaAsa.Core.Configuration;

namespace CasaAsa.Business.Component.Notifications
{
    public interface IMailComponent
    {
        void SendMail(Mail mail);
    }
}