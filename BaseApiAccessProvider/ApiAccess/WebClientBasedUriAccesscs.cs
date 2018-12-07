using CommonApiAccessProvider.Abstracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonApiAccessProvider.ApiAccess
{
    public class WebClientBasedApiAccess : BaseApiAccessProvider
    {
        public WebClient _wc;

        public WebClientBasedApiAccess(string baseAddress,string[] headers = null)  {
            _wc = new WebClient()
            {
                BaseAddress = baseAddress
                
            };
            if (headers != null)
            {
                foreach (var h in headers)
                {
                    _wc.Headers.Add(h);
                }
            }
            
            
        }
        public override string CallUri(string uri)
        {
            Stream data = _wc.OpenRead(uri);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            return s;
        }

       
    }
}
