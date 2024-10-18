namespace ClientSVH.Core.Models
{
    public class HistoryPkg
    {
        private HistoryPkg( int pid, int oldst, int newst, string comment, DateTime createDate)
        {
           
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
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public static HistoryPkg Create( int pid, int oldst, int newst, string comment, DateTime createDate)
        {
            var historyPkg = new HistoryPkg( pid, oldst, newst, comment, createDate);
            return historyPkg;
        }

    }
}
