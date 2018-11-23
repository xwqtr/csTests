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
    class WebClientBasedUriAccess : BaseApiAccessProvider
    {

        public string BaseAddress { get; set; }

        public WebClient _wc;

        public WebClientBasedUriAccess(WebClient wc,string apiKey) :base(apiKey) {
            _wc = wc;
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
