using ClientSVH.Core.Models;


namespace ClientSVH.Application.CollectingListToXml.HendlersElem
{
    public class TaskItemElem : TaskItemBase
    {
        public TaskItemElem(Document inDocx)
        {
            TaskDocs = inDocx;
        }
    }
}