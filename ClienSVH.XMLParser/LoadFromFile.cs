using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Repositories;

using ClientSVH.Core.Models;
using ClientSVH.DocsRecordCore.Abstraction;
using ClientSVH.DocsRecordCore.Models;
using System.Data;

using System.Security.Cryptography;
using System.Text;
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
                        var Doc = ClientSVH.Core.Models.Document.Create(doc_1, DocId, doc.num, DateTime.Parse(doc.dat),
                                      doc.doctext, doc.doctext.Length, GetHashMd5(doc.doctext), GetSha256(doc.doctext),
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

       

        private static string GetHashMd5(string text)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(text))
            {
                var md5 = MD5.Create();
                var hash = md5?.ComputeHash(Encoding.UTF8.GetBytes(text));
                if(hash!=null) result = Convert.ToBase64String(hash);
            }
            return result;
        }
        private static string GetSha256(string text)
        {
            var sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var result = hash?.ComputeHash(Encoding.UTF8.GetBytes(text));
                if (result != null)
                {
                    for (int i = 0; i < result.Length; i++)
                        sb.Append(result[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }
       
    }
}
