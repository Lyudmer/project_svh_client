using ClientSVH.Core.Models;
using ClientSVH.DocsRecordCore.Abstraction;
using System.Xml.Linq;


namespace ClientSVH.Application.CollectingListToXml.HendlersElem
{
    public class TasksHandlerElem(IDocRecordRepository docRecRepository, List<Document> inDocs) : IHandlerElem
    {

        public IDocRecordRepository _docRecRepository = docRecRepository;

        private readonly List<TaskItemElem> _queueTasksElem = [];

        public void ProcessQueue(ref XElement elem)
        {

            foreach (var item in inDocs)
            {
                var taskitem = new TaskItemElem(item);
                _queueTasksElem.Add(taskitem);
            }
            _queueTasksElem.ForEach(RunTask);

            WaitAllTasks(ref elem);

        }

        private void WaitAllTasks(ref XElement elem)
        {
            var tasksForWait = GetTasksForWait();
            Task.WaitAll(tasksForWait);

            foreach (var result in _queueTasksElem)
            {
                if (result is not null)
                    elem.Add(result);
            }
        }

        private Task[] GetTasksForWait()
        {
            return _queueTasksElem
               .Select(t => t.Task)
               .ToArray();
        }

        private void RunTask(TaskItemElem item)
        {
            item.Task = Task.Run(async () => item.TaskElem = await CreateElemXmlFromDoc(item.TaskDocs));
        }

        public async Task<XElement> CreateElemXmlFromDoc(Document inDoc)
        {
            var docRecord = await _docRecRepository.GetByDocId(inDoc.DocId);

            if (docRecord is not null)
            {
                XElement elem_doc = XElement.Parse(docRecord.DocText.ToString());
                elem_doc.SetAttributeValue("docid", inDoc.DocId.ToString());
                elem_doc.SetAttributeValue("doctype", inDoc.DocType);
                return elem_doc;
            }

            return null;
        }


    }


}