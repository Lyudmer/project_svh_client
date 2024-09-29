namespace ClientSVH.Core.Models
{ 
    public class Document
    {
        private Document(int id, Guid docId, string number, DateTime docDate,
                         string modeCode,string docType, int sizeDoc,string idmd5,string idSha256,
                         int pid, DateTime createDate, DateTime modifyDate)
        {
            Id = id;
            DocId = docId;
            Number=number;
            DocDate=docDate;
            ModeCode=modeCode;
            DocType = docType;
            SizeDoc=sizeDoc;
            Idmd5=idmd5;
            IdSha256=idSha256;
            CreateDate = createDate;
            ModifyDate = modifyDate;
            Pid=pid;
        }

        public int Id { get; set; }
        public Guid DocId { get; set; }
        public DateTime CreateDate { get; set; }= DateTime.Now;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public string Number { get; set; }=string.Empty;
        public DateTime DocDate { get; set; }
        public string ModeCode { get; set; } = string.Empty;
        public string DocType { get; set; } = string.Empty;
        public int SizeDoc { get; set; }
        public string Idmd5 { get; set; } = string.Empty;
        public string IdSha256 { get; set; } = string.Empty;
        public int Pid { get; set; }

        public static Document Create(int id, Guid docId,string number, DateTime docDate, 
                                      string modeCode, string docType, int sizeDoc, string idmd5, string idSha256, 
                                      int pid, DateTime createDate, DateTime modifyDate)
        {
            var document = new Document(id,docId,number,docDate,modeCode,docType, sizeDoc,
                                        idmd5,idSha256,pid,createDate, modifyDate);

            return document;
        }
        
    }
}
