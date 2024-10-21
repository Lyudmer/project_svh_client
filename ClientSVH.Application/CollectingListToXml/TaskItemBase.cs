using ClientSVH.Core.Models;
using ClientSVH.DocsRecordCore.Abstraction;
using System.Xml.Linq;

namespace ClientSVH.Application.CollectingListToXml
{
    public class TaskItemBase
    {
        public Task Task { get; set; }
        public Document TaskDocs { get; set; }
        public XElement TaskElem { get; set; }
    }
}