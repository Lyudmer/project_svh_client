namespace ClientSVH.DataAccess.Entities
{
    public class StatusGraphEntity
    {
        public int OldSt { get; set; }
        public int NewSt { get; set; }
        public int StatusId { get; set; }
        public StatusEntity? Status { get; set; }
  
    }

}
