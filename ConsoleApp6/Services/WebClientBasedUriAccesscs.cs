using ConsoleApp6.Abstracts;
using ConsoleApp6.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6.Services
{
    public class WebClientBasedUriAccess : BaseApiAccessProvider
    {
        public WebClient _wc;

        public WebClientBasedUriAccess(string baseAddress,string apiKey) :base(apiKey) {
            _wc = new WebClient()
            {
                BaseAddress = baseAddress
            };
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
