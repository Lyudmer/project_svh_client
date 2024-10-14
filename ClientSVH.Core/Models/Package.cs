namespace ClientSVH.Core.Models
{
    public class Package
    {
        private Package( Guid userId, int statusId, DateTime createDate, DateTime modifyDate)
        {
            UserId = userId;
            StatusId = statusId;
            
            CreateDate = createDate;
            ModifyDate = modifyDate;
        }
        public int Pid { get; set; }
        public Guid UserId { get; set; }
        public int StatusId { get; set; }
     
        public Guid UUID { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public static Package Create( Guid userId, int statusId, DateTime createDate, DateTime modifyDate)
        {
            var package = new Package(userId, statusId,  createDate, modifyDate);
            return package;
        }
        public ICollection<Document> Documents { get; set; } = [];
    }
}
