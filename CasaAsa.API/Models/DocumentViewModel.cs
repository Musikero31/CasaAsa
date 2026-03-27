namespace CasaAsa.API.Models
{
    public class DocumentViewModel
    {
        public int? DocumentId { get; set; }
        public byte[]? DocumentFile { get; set; }
        public string? FileName { get; set; }
        public string? DocumentType { get; set; }
        public string? FileExtension { get; set; }
        public string? MimeType { get; set; }
        public string? DocumentPath { get; set; }
        public string? Base64DocumentFile { get; set; }
    }
}
