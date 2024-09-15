
namespace ClientSVH.DataAccess.Entities
{
    public class StatusEntity
    {
        public int Id { get; set; }
        public string? StatusName { get; set; }
        public bool RunWf { get; set; }
        public bool MkRes { get; set; }
        public bool SendMess { get; set; }   

        public int OldSt { get; set; }
        public int NewSt { get; set; }
        public StatusGraphEntity? StatusGraph { get; set; }
        
    }
}
