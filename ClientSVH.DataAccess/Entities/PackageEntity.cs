namespace ClientSVH.DataAccess.Entities
{
    public class PackageEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public Guid UUID { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public ICollection<DocumentEntity> Documents { get; set; } =  [];
        public int StatusId { get; set; }
        public StatusEntity Status { get; set; }
        
    }
}
