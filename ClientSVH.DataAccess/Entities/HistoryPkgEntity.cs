
namespace ClientSVH.DataAccess.Entities
{
    public class HistoryPkgEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Pid { get; set; }
        public int Oldst { get; set; }
        public int Newst { get; set; }
        public StatusEntity? Status { get; set; }
        public string Comment { get; set; }=string.Empty;
        public DateTime CreateDate { get; set; }

    }
}
