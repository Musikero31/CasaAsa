using CasaAsa.Core.BusinessModels;

namespace CasaAsa.Business.Component.Document
{
    public interface IDocumentComponent
    {
        Task RemoveDocumentAsync(int docId);
        Task<Documents> RetrieveDocumentAsync(int docId);
        Task<int> UploadDocumentAsync(Documents document);
    }
}