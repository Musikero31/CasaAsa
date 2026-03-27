using AutoMapper;
using CasaAsa.Business.Constants;
using CasaAsa.Data.Repository;
using Microsoft.Extensions.Logging;
using CoreModel = CasaAsa.Core.BusinessModels;
using DataModel = CasaAsa.Data.Models;

namespace CasaAsa.Business.Component.Document
{
    public class DocumentComponent : IDocumentComponent
    {
        private readonly IRepository<DataModel.Documents> _docRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentComponent> _logger;

        public DocumentComponent(IRepository<DataModel.Documents> docRepo,
                                 IMapper mapper,
                                 ILogger<DocumentComponent> logger)
        {
            _docRepo = docRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> UploadDocumentAsync(CoreModel.Documents document)
        {
            var fileBytes = await File.ReadAllBytesAsync(document.DocumentPath);

            var data = _mapper.Map<DataModel.Documents>(document);
            data.DocumentFile = fileBytes;
            data.MimeType = RetrieveMimeType(document.DocumentPath);
            data.FileExtension = Path.GetExtension(document.DocumentPath);
            data.FileName = Path.GetFileName(document.DocumentPath);

            await _docRepo.AddAsync(data);
            await _docRepo.SaveChangesAsync();

            return data.Id;
        }

        public async Task<CoreModel.Documents> RetrieveDocumentAsync(int docId)
        {
            var data = await _docRepo.GetByIdAsync(docId);

            return _mapper.Map<CoreModel.Documents>(data);
        }

        public async Task RemoveDocumentAsync(int docId)
        {
            var data = await _docRepo.GetByIdAsync(docId)
                ?? throw new ArgumentNullException($"Document id {docId} not found.");

            await _docRepo.RemoveAsync(data);
            await _docRepo.SaveChangesAsync();

            _logger.LogInformation($"Document id {docId} has been deleted.");
        }

        private string RetrieveMimeType(string documentPath)
        {
            string result = string.Empty;

            var extension = Path.GetExtension(documentPath);

            switch (extension)
            {
                case ".gif":
                    result = MimeTypes.GIF; 
                    break;
                case ".jpeg":
                case ".jpg":
                    result = MimeTypes.JPEG;
                    break;
                case ".png":
                    result = MimeTypes.PNG;
                    break;
                case ".pdf":
                    result = MimeTypes.PDF;
                    break;
                case ".doc":
                    result = MimeTypes.DOC;
                    break;
                case ".docx":
                    result = MimeTypes.DOCX;
                    break;
                default:
                    break;
            }

            return result;
        }


    }
}
