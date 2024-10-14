using System;

namespace ClientSVH.Core.Models
{
    public class Package
    {
        private Package(int pid, Guid userId, int statusId, Guid uuid, DateTime createDate, DateTime modifyDate)
        {
            Pid = pid;
            UserId = userId;
            StatusId = statusId;
            UUID = uuid;    
            CreateDate = createDate;
            ModifyDate = modifyDate;
        }
        public int Pid { get; set; }
        public Guid UserId { get; set; }
        public int StatusId { get; set; }
     
        public Guid UUID { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public static Package Create(int pid, Guid userId, int statusId, Guid uuid, DateTime createDate, DateTime modifyDate)
        {
            var package = new Package(pid,userId, statusId, uuid, createDate, modifyDate);
            return package;
        }
        public ICollection<Document> Documents { get; set; } = [];
    }
}
