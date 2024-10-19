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
        public async Task<int> LoadFileXml(Guid userId, string inFile)
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
                   
                    foreach (var xDoc in xPkg)
                    {
                        var LastDocId = _docRepository.GetLastDocId().Result + 1;
                        
                        var tdoc = xDoc.Name.LocalName;
                        var num = xDoc.Elements().Elements("RegNum").FirstOrDefault()?.Value.ToString();
                        var dat = xDoc.Elements().Elements("RegDate").FirstOrDefault()?.Value.ToString();
                        var doctext = xDoc.ToString();
                        string docCode = tdoc.Contains("CONOSAMENT") ? "02011" : "09999";
                        DateTime DocDate = DateTime.Now;
                        if (dat is not null)
                            _ = DateTime.TryParse(dat, out DocDate);

                        var Doc = Document.Create(LastDocId, Guid.NewGuid(), (num is not null)?num:string.Empty, DocDate, docCode,
                                      tdoc, doctext.Length, DopFunction.GetHashMd5(doctext),
                                      DopFunction.GetSha256(doctext),
                                      Pid, DateTime.UtcNow, DateTime.UtcNow);

                        
                        Doc = await _docRepository.Add(Doc);
                        if (Doc is not null)
                        {
                            DocRecord dRecord = DocRecord.Create(Doc.DocId, doctext);
                            var dRecordId = await _docRecordRepository.AddRecord(dRecord);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
                
            }

            return Pid;
        }

       

      
       
    }
}
