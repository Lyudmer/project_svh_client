namespace ClientSVH.Core.Models
{
    public class HistoryPackages
    {
        private HistoryPackages(int pid, int oldst, int newst, string comment)
        {
            Pid = pid;
            Oldst=oldst;
            Newst = newst;
            Comment = comment;
        }
        public int Pid { get; set; }
        public int Oldst { get; set; }
        public int Newst { get; set; }
        public string Comment { get; set; }=string.Empty;
        public static HistoryPackages Create(int pid, int oldst, int newst, string comment)
        {
            var historyPkg = new HistoryPackages(pid, oldst, newst,  comment);
            return historyPkg;
        }

    }
}
