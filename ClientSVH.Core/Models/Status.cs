using System.Security.Cryptography;
using System;

namespace ClientSVH.Core.Models
{
    public class Status
    {
        private Status(int id, string statusName, bool runWf, bool mkRes, bool sendMess)
        {
            Id = id;
            StatusName = statusName;
            RunWf = runWf;
            MkRes = mkRes;
            SendMess = sendMess;
        }
        public int Id { get; set; }
        public string? StatusName { get; set; }
        public bool RunWf { get; set; }
        public bool MkRes { get; set; }
        public bool SendMess { get; set; }
      
        public static Status Create(int id, string statusName, bool runWf, bool mkRes, bool sendMess)
        {
            var status = new Status(id, statusName, runWf, mkRes,sendMess);
            return status;
        }
      
    }
}
