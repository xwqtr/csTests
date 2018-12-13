using CommonApiAccessProvider.Abstracts;
using CommonApiAccessProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CommonApiAccessProvider.ApiAccess
{
    public class WebClientBasedApiAccess : BaseApiAccessProvider
    {
        public WebClient _wc;

        public WebClientBasedApiAccess(IApiAccessParameters parameters)
        {
            _wc = new WebClient()
            {
                BaseAddress = parameters.baseAddress

            };
            if (parameters.headers != null)
            {
                foreach (var h in parameters.headers)
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
