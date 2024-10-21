using ClientSVH.Core.Models;
using ClientSVH.DocsRecordCore.Abstraction;
using System.Xml.Linq;

namespace ClientSVH.Application.CollectingListToXml.Hendlers
{
    public class TaskItemDoc : TaskItemBase
    {

        public TaskItemDoc(XElement inDocx)
        {
            TaskElem = inDocx;
        }
    }
}
