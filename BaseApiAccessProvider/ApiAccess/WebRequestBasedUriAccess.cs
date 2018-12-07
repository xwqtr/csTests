namespace CommonApiAccessProvider.ApiAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.IO;
    using CommonApiAccessProvider.Abstracts;
    public class WebRequestBasedApiAccess : BaseApiAccessProvider
    {
        private readonly string _baseAddress;
        private readonly string[] _headers;
        public WebRequestBasedApiAccess(string baseAddress,string []headers=null){
            _baseAddress = baseAddress;
            _headers = headers;
        }
        
        public override string CallUri(string uri)
        {
            var requestUri = new Uri(new Uri(_baseAddress), uri);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            foreach (var h in _headers)
            {
                request.Headers.Add(h);
            }
            
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

    }
}
