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
                    var pid = await _pkgServices.Add(userId, Pkg);
                    Pid = pid;
                    foreach (XElement xDoc in xPkg.Elements())
                    {
                      

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
