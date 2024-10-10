using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Repositories;

using ClientSVH.Core.Models;
using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.DocsRecordCore.Models;
using ServerSVH.Application.Common;
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
        public async Task<int> LoadFile(Guid userId, string InFile)
        {
            int Pid = 0;
            try
            {

                XDocument xFile = XDocument.Load(InFile);
                var xPkg = xFile.Element("Package")?
                    .Elements("*").Where(p => p.Attribute("ctmtd")?.Value == "CfgName");
                
                if (xPkg is not null)
                {
                    //create package
                    var pid_1 = await _pkgRepository.GetLastPkgId() + 1;
                    var Pkg = Package.Create(pid_1, userId, 0, Guid.NewGuid(), DateTime.Now, DateTime.Now);

                    Pkg = await _pkgRepository.Add(Pkg);
                    Pid = Pkg.Pid;

                    var xDocs = from xDoc in xPkg?.AsParallel().Elements()
                                select new
                                {
                                    tdoc = xDoc.Name?.LocalName,
                                    num = xDoc.Elements().Elements().FirstOrDefault(n => n.Name == "RegNum")?.Value,
                                    dat = xDoc.Elements().Elements().FirstOrDefault(n => n.Name == "RegDate")?.Value,
                                    doctext = xDoc.ToString()

                                };
                    foreach (var doc in xDocs)
                    {
                        var doc_1 = await _docRepository.GetLastDocId() + 1;
                        var DocId = Guid.NewGuid();
                        var Doc = Document.Create(doc_1, DocId, doc.num, DateTime.Parse(doc.dat),"",
                                      doc.tdoc, doc.doctext.Length, DopFunction.GetHashMd5(doc.doctext),
                                      DopFunction.GetSha256(doc.doctext),
                                      Pid, DateTime.Now, DateTime.Now);

                        
                        Doc = await _docRepository.Add(Doc);
                        if (Doc is not null)
                        {
                            DocRecord dRecord = DocRecord.Create(Guid.NewGuid(), Doc.DocId, doc.doctext, DateTime.Now, DateTime.Now);
                            var dRecordId = await _docRecordRepository.Add(dRecord);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
                //
            }

            return Pid;
        }

       

      
       
    }
}
