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
        public WebRequestBasedApiAccess(IApiAccessParameters parameters)
        {
            _baseAddress = parameters.baseAddress;
            _headers = parameters.headers;
        }
        
        public override string CallUri(string uri)
        {
            var requestUri = new Uri(new Uri(_baseAddress), uri);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            if (_headers != null)
            {
                foreach (var h in _headers)
                {
                    request.Headers.Add(h);
                }
            }
            
            
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

    }
}
