using ClientSVH.Application.Interfaces.Auth;
using ClientSVH.Core.Abstraction.Services;
using ClientSVH.Core.Models;
using ClientSVH.DocsBodyCore.Abstraction;
using ClientSVH.DocsBodyCore.Models;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;


namespace ClienSVH.XMLParser
{
    public class LoadFromFile(IPackagesServices pkgServices,
        IDocumentsServices docServices,
        IDocRecordRepository docRecordRepository) : ILoadFromFile
    {
        private readonly IPackagesServices _pkgServices = pkgServices;
        private readonly IDocumentsServices _docServices = docServices;
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
                    var pid_1 = await _pkgServices.GetLastPkgId() + 1;
                    var Pkg = Package.Create(pid_1, userId, 0, Guid.NewGuid(), DateTime.Now, DateTime.Now);

                    Pkg = await _pkgServices.Add(Pkg);
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
                        var doc_1 = await _docServices.GetLastDocId() + 1;
                        var DocId = Guid.NewGuid();
                        var Doc = Document.Create(doc_1, DocId, doc.num, DateTime.Parse(doc.dat),
                                      doc.doctext, doc.doctext.Length, GetHashMd5(doc.doctext), GetSha256(doc.doctext),
                                      Pid, DateTime.Now, DateTime.Now);

                        DocRecord dRecord = DocRecord.Create(Guid.NewGuid(), DocId, doc.doctext, DateTime.Now, DateTime.Now);
                        Doc = await _docServices.Add(Doc, dRecord);
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
        public static string GetHashMd5(string text)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(text))
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));

                result = Convert.ToBase64String(hash);
            }
            return result;
        }
        public static string GetSha256(string text)
        {
            var sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                for (int i = 0; i < result.Length; i++)
                    sb.Append(result[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
