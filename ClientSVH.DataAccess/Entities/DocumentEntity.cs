
using ClientSVH.DocsBodyDataAccess.Entities;

namespace ClientSVH.DataAccess.Entities
{ 
    public class DocumentEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }=string.Empty;
        public DateTime DocDate { get; set; }
        public string ModeCode { get; set; } = string.Empty;
        public int SizeDoc { get; set; }
        public string Idmd5 { get; set; } = string.Empty;       
        public string IdSha256 { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Pid { get; set; }
        public PackageEntity? Package { get; set; } 
        public Guid DocId { get; set; }
        public DocRecordEntity? DocRecord { get; set; }
    }
}
