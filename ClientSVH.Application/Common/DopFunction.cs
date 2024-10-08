using System.Security.Cryptography;
using System.Text;


namespace ClientSVH.Application.Common
{
 
    public class DopFunction
    {

        public static string GetHashMd5(string text)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(text))
            {
                var md5 = MD5.Create();
                var hash = md5?.ComputeHash(Encoding.UTF8.GetBytes(text));
                if (hash != null) result = Convert.ToBase64String(hash);
            }
            return result;
        }
        public static string GetSha256(string text)
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
