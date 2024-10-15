using System;

namespace ClientSVH.Core.Models
{
    public class Package
    {
        private Package(int id, Guid userId, int statusId, Guid uuId, DateTime createDate, DateTime modifyDate)
        {
            Id = id;
            UserId = userId;
            StatusId = statusId;
            UUID = uuId;    
            CreateDate = createDate;
            ModifyDate = modifyDate;
        }
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int StatusId { get; set; }
     
        public Guid UUID { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifyDate { get; set; } = DateTime.UtcNow;
        public static Package Create(int id, Guid userId, int statusId, Guid uuId, 
                                     DateTime createDate, DateTime modifyDate)
        {
            var package = new Package(id,userId, statusId, uuId, createDate, modifyDate);
            return package;
        }
        public ICollection<Document> Documents { get; set; } = [];
    }
}
