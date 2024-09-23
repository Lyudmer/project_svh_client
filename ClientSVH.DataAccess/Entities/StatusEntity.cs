
namespace ClientSVH.DataAccess.Entities
{
    public class StatusEntity
    {
        public int Id { get; set; }
        public string? StatusName { get; set; }
        public bool RunWf { get; set; }
        public bool MkRes { get; set; }
        public bool SendMess { get; set; }   

        
    }
}
