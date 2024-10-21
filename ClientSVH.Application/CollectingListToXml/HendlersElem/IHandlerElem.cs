using System;
using System.Xml.Linq;

namespace ClientSVH.Application.CollectingListToXml.HendlersElem
{
    public interface IHandlerElem
    {
        void ProcessQueue(ref XElement elem);

    }

}