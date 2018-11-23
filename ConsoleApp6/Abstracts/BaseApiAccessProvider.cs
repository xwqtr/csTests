using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using ConsoleApp6.Interfaces;

namespace ConsoleApp6.Abstracts
{
    public abstract class BaseApiAccessProvider : IApiAccessProvider
    {
       
        private string _apiKey = "&appid=";
        public BaseApiAccessProvider(string apiId) {
            _apiKey += apiId; 
        }
        

        public abstract string CallUri(string uri);

        public T GetData<T>(string uri) {
            uri += _apiKey;
            return JsonConvert.DeserializeObject<T>(this.CallUri(uri));
        }
    }
}
