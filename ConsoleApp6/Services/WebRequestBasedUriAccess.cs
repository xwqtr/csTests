using ConsoleApp6.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ConsoleApp6.Services
{
    public class WebRequestBasedUriAccess : BaseApiAccessProvider
    {
        private readonly string _baseAddress;
        public WebRequestBasedUriAccess(string baseAddress, string apiId) : base(apiId) {
            _baseAddress = baseAddress;
        }
        public override string CallUri(string uri)
        {
            var requestUri = new Uri(new Uri(_baseAddress), uri);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        
    }
}
