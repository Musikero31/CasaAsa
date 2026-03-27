namespace CasaAsa.API.Models
{
    public class DocumentUploadRequest
    {
        public int? DocumentId { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentType { get; set; }
        public string? FileType { get; set; }
        public string? MimeType { get; set; }
        public string? DocumentPath { get; set; }
    }
}
