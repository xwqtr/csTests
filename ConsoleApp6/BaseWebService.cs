using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
namespace ConsoleApp6
{
    public abstract class BaseWebService : IBaseWebService
    {
        private readonly WebClient _wc;
        public BaseWebService(WebClient wc) {
            _wc = wc;
                 
        }
        public T GetDataWebClient<T>(string uri) {
            Stream data = _wc.OpenRead(uri);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            Console.WriteLine(s);
            data.Close();
            reader.Close();
            return JsonConvert.DeserializeObject<T>(s);
        }
    }
}
