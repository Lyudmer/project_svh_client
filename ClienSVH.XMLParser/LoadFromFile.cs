using ClientSVH.Application.Services;
using ClientSVH.Core.Models;
using System.Xml.Linq;

namespace ClienSVH.XMLParser
{
    public class  LoadFromFile(PackagesServices pkgServices)
    {
        private readonly PackagesServices _pkgServices = pkgServices;
        public async Task<int> LoadFile(Guid userId,string InFile)
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
                    var pid_1 = await _pkgServices.GetLastPkgId();
                    var Pkg = Package.Create(pid_1, userId, 0, Guid.NewGuid(), DateTime.Now, DateTime.Now);
                    Pkg = await _pkgServices.Add(Pkg);
                    Pid = Pkg.Pid;
                    var xDocs = from xDoc in xPkg?.AsParallel().Elements()
                                select new
                                {
                                    tdoc = xDoc.Name?.LocalName,
                                    num = xDoc.Elements().Elements().FirstOrDefault(n => n.Name == "RegNum")?.Value,
                                    dat = xDoc.Elements().Elements().FirstOrDefault(n => n.Name == "RegDate")?.Value
                                };
                    foreach (var doc in xDocs) 
                    {
                        var doc_1 = await _docServices.GetLastDocId();
                       // var Doc =Document.Create()
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
