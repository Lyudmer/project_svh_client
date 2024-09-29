namespace ClientSVH.Core.Models
{
    public class HistoryPkg
    {
        private HistoryPkg(Guid id, int pid, int oldst, int newst, string comment, DateTime createDate)
        {
            Id = id;
            Pid = pid;
            Oldst = oldst;
            Newst = newst;
            Comment = comment;
            CreateDate = createDate;
        }
        public Guid Id { get; set; }
        public int Pid { get; set; }
        public int Oldst { get; set; }
        public int Newst { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public static HistoryPkg Create(Guid Id, int pid, int oldst, int newst, string comment, DateTime createDate)
        {
            var historyPkg = new HistoryPkg(Id, pid, oldst, newst, comment, createDate);
            return historyPkg;
        }

    }
}
