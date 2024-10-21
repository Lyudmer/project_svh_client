using ClientSVH.Application.CollectingListToXml.Hendlers;
using ClientSVH.Application.Common;
using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Repositories;

using ClientSVH.Core.Models;
using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.DocsRecordCore.Models;

using System.Data;

using System.Xml.Linq;



namespace ClienSVH.XMLParser
{
    public class LoadFromFile(IPackagesRepository pkgRepository,
        IDocumentsRepository docRepository, IDocRecordRepository docRecordRepository) : ILoadFromFile
    {
        private readonly IPackagesRepository _pkgRepository = pkgRepository;
        private readonly IDocumentsRepository _docRepository = docRepository;
        private readonly IDocRecordRepository _docRecordRepository = docRecordRepository;
        public async Task<string> LoadFileXml(Guid userId, string inFile)
        {
            int Pid = _pkgRepository.GetLastPkgId().Result+1;
            try
            {
                XDocument xFile = XDocument.Parse(inFile.Trim());

                var xPkg = xFile.Element("Package")?
                    .Elements().Where(p => p.Attributes().Where(a => a.Name.LocalName.Contains("CfgName")).FirstOrDefault()?.Value is not null);

                if (xPkg is not null)
                {
                    //create package
                    var Pkg = Package.Create(Pid, userId, 0, Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow);
                    Pkg = await _pkgRepository.Add(Pkg);
                    Pid = Pkg.Id;
                    int cXmlDoc = xPkg.Count();
                    var resProc = new TasksHandlerDoc(_docRecordRepository, _docRepository,xPkg,Pid);
                    var resDoc= resProc.ProcessQueueDoc();
                    if (cXmlDoc != resDoc) return $"В исходном xml документов{cXmlDoc}, загружено {resDoc}";
                }
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
                
            }

            return $"Создан пакет {Pid}";
        }

    }
}
