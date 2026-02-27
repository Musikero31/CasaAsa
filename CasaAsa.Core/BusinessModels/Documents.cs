namespace CasaAsa.Core.BusinessModels
{
    public class Documents
    {
        public int DocumentId { get; set; }
        public byte[] DocumentFile { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string FileType { get; set; }
        public string MimeType { get; set; }
    }
}
